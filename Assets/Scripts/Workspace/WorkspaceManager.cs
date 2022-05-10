using System;
using System.Linq;
using pladdra_app.Assets.Scripts.Pipelines;
using pladdra_app.Assets.Scripts.UXHandlers;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public interface IWorkspaceScene
    {
        GameObject plane { get; }
        IWorkspaceObjectsManager objectsManager { get; }
    }

    public class WorkspaceManager : MonoBehaviour, IWorkspaceManager, IWorkspaceScene
    {
        public IUXHandler uxHandler { get; set; }
        public GameObject workspaceOrigin;
        public GameObject itemPrefab;
        public IWorkspaceObjectsManager objectsManager { get; set; }
        public WorkspaceConfiguration configuration { get; set; }

        public GameObject plane { get; set; }

        public WorkspaceManager()
        {
            uxHandler = new NullUXHandler();
        }


        public void SetUXhandler(IUXHandler handler)
        {
            uxHandler.Deactivate(this);
            uxHandler = handler ?? new NullUXHandler();
            uxHandler.Activate(this);
        }


        private void Awake()
        {
            objectsManager = new WorkspaceObjectsManager(itemPrefab);
        }

        public void Activate(WorkspaceConfiguration wc)
        {
            configuration = wc;

            workspaceOrigin.transform.position = wc.origin.position;
            workspaceOrigin.transform.rotation = wc.origin.rotation;

            plane = FindObjectOfType<PlaneFactory>()
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

            // FAKE CODE
            SetUXhandler(new CompositeUXHandler(new AllowUserToPositionPlane()));
        }
    }
}