using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using pladdra_app.Assets.Scripts.Data;
using pladdra_app.Assets.Scripts.Data.Dialogs;
using pladdra_app.Assets.Scripts.Workspace;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Pipelines
{
    public class Pipeline
    {
        public Func<IDialogProjectRepository> CreateDialogProjectRepository { get; set; }

        public Func<WebResourceManager> CreateWebResourceManager { get; set; }

        public Func<DialogResource, GameObject, IWorkspaceResource> CreateWorkspaceResource { get; set; }

        public Func<DialogProject, IEnumerable<IWorkspaceResource>, IWorkspaceResourceCollection> CreateWorkspaceResourceCollection { get; set; }

        public Pipeline()
        {
            CreateDialogProjectRepository = () => new SampleDialogProjectRepository(Application.temporaryCachePath);
            CreateWebResourceManager = () => new WebResourceManager(Application.temporaryCachePath);
            CreateWorkspaceResource = (DialogResource resource, GameObject prefab) => new WorkspaceResource { prefab = prefab, resourceID = resource.Url };
            CreateWorkspaceResourceCollection = (DialogProject project, IEnumerable<IWorkspaceResource> items) => new WorkspaceResourceCollection { resources = items };
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
                spaceItems = /* fake */ allResources
                    .Select((resource, index) => new WorkspaceItemInSpace
                    {
                        resourceId = resource.resourceID,
                        position = new Vector3(index, 0, 0),
                        scale = new Vector3(1, 1, 1),
                        rotation = new Quaternion()
                    }).ToList()
            };

            var plane = new WorkspacePlane
            {
                dimensions = new Vector2(6, 12)
            };

            Debug.Log("[pipline] creating workspace...");
            var resourceCollection = CreateWorkspaceResourceCollection(project, modelItems.Concat(markerItems)
                    .ToList());

            var configuration = new WorkspaceConfiguration
            {
                cosmos = cosmos,
                plane = plane,
                resourceCollection = resourceCollection
            };


            callback(configuration);
        }
    }
}
