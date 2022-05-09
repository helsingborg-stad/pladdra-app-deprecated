using System;
using System.Linq;
using pladdra_app.Assets.Scripts.Pipelines;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{

    public class WorkspaceManager : MonoBehaviour, IWorkspaceManager
    {
        public GameObject workspaceOrigin;
        public GameObject itemPrefab;
        public IWorkspaceObjectsManager objectsManager { get; private set; }
        public WorkspaceConfiguration configuration { get; set; }


        private void Awake()
        {
            objectsManager = new WorkspaceObjectsManager(itemPrefab);
        }

        public void Activate(WorkspaceConfiguration wc)
        {
            configuration = wc;

            workspaceOrigin.transform.position = wc.origin.position;
            workspaceOrigin.transform.rotation = wc.origin.rotation;

            var plane = FindObjectOfType<PlaneFactory>()
                .SpawnPlane(configuration.plane.width, configuration.plane.height);

            plane.transform.SetParent(workspaceOrigin.transform);

            var spawns = configuration.cosmos.spaceItems
                .Select(ci => new
                {
                    resource = configuration.resourceCollection.TryGetResource(ci.resourceId),
                    ci
                })
                .Where(o => o.resource != null);

            foreach (var spawn in spawns)
            {
                objectsManager.SpawnItem(
                    plane,
                    spawn.resource,
                    spawn.ci.position,
                    spawn.ci.rotation,
                    spawn.ci.scale);
            }
        }
    }
}