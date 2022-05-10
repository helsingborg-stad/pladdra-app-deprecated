using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Dialogs;
using UnityEngine;
using Utility;
using Workspace;

namespace Pipelines
{
    public class Pipeline
    {
        public Func<IDialogProjectRepository> CreateDialogProjectRepository { get; set; }

        public Func<IWebResourceManager> CreateWebResourceManager { get; set; }

        public Func<DialogResource, GameObject, IWorkspaceResource> CreateWorkspaceResource { get; set; }

        public Func<DialogProject, IEnumerable<IWorkspaceResource>, IWorkspaceResourceCollection> CreateWorkspaceResourceCollection { get; set; }

        public Pipeline()
        {
            CreateDialogProjectRepository = () => new SampleDialogProjectRepository(Application.temporaryCachePath);
            CreateWebResourceManager = () => new WebResourceManager(Application.temporaryCachePath);
            CreateWorkspaceResource = (resource, prefab) => new WorkspaceResource { Prefab = prefab, ResourceID = resource.Url };
            CreateWorkspaceResourceCollection = (project, items) => new WorkspaceResourceCollection { Resources = items };
        }
        public IEnumerator LoadWorkspace(Action<WorkspaceConfiguration> callback)
        {
            Debug.Log("[pipline] loading external project...");
            var repo = CreateDialogProjectRepository();
            DialogProject project = null;
            yield return new LoadExternalProject(repo, p => project = p);
            Debug.Log("[pipline] done loading external project");

            Debug.Log("[pipline] mapping external resources to local...");
            var wrm = CreateWebResourceManager();
            Dictionary<string, string> url2path = null;
            yield return new MapExternalResourceToLocalPaths(wrm, project, p => url2path = p);
            Debug.Log("[pipline] done mapping external resources to local");

            Debug.Log("[pipline] loading 3d model prefabs...");
            var path2model = new Dictionary<string, GameObject>();
            foreach (var path in url2path.Values)
            {
                yield return new Load3dModel(path, go =>
                {
                    Debug.Log($"[pipline] done loading 3d model {path}");
                    path2model[path] = go;
                });
            }
            Debug.Log("[pipline] done loading 3d model prefabs");

            Debug.Log("[pipline] creating workspace items...");
            var modelItems = project.Resources
                    .Where(resource => resource.Type == "model")
                    .Select(resource => new { resource, gameObject = path2model.TryGet(url2path.TryGet(resource.Url)) })
                    .Where(o => o.gameObject != null)
                    .Select(o => CreateWorkspaceResource(o.resource, o.gameObject))
                    .Where(item => item != null)
                    .ToList();

            var markerItems = project.Resources
                    .Where(resource => resource.Type == "marker")
                    .Select(resource => new { resource, gameObject = path2model.TryGet(url2path.TryGet(resource.Url)) })
                    .Where(o => o.gameObject != null)
                    .Select(o => CreateWorkspaceResource(o.resource, o.gameObject))
                    .Where(item => item != null)
                    .ToList();
            Debug.Log("[pipline] done creating workspace items");


            var allResources = modelItems.Concat(markerItems);

            Debug.Log("[pipline] faking cosmos");
            var cosmos = new WorkspaceCosmos
            {
                SpaceItems = /* fake */ allResources
                    .Select((resource, index) => new WorkspaceItemInSpace
                    {
                        ResourceId = resource.ResourceID,
                        Position = new Vector3(index, 0, 0),
                        Scale = new Vector3(1, 1, 1),
                        Rotation = new Quaternion()
                    }).ToList()
            };

            var plane = new WorkspacePlane
            {
                Width = 6,
                Height = 12
            };

            Debug.Log("[pipline] creating workspace...");
            var resourceCollection = CreateWorkspaceResourceCollection(project, modelItems.Concat(markerItems)
                    .ToList());

            var configuration = new WorkspaceConfiguration
            {
                Origin = new WorkspaceOrigin(),
                Cosmos = cosmos,
                Plane = plane,
                ResourceCollection = resourceCollection
            };


            callback(configuration);
        }
    }
}
