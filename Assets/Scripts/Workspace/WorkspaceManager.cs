using System.Linq;
using UnityEngine;
using UXHandlers;

namespace Workspace
{
    public class WorkspaceManager : MonoBehaviour, IWorkspaceManager, IWorkspaceScene
    {
        public IUxHandler UxHandler { get; set; }
        public GameObject workspaceOrigin;
        public GameObject itemPrefab;
        public IWorkspaceObjectsManager ObjectsManager { get; set; }
        private WorkspaceConfiguration Configuration { get; set; }

        public GameObject Plane { get; set; }

        public WorkspaceManager()
        {
            UxHandler = new NullUxHandler();
        }


        public void SetUXhandler(IUxHandler handler)
        {
            UxHandler.Deactivate(this);
            UxHandler = handler ?? new NullUxHandler();
            UxHandler.Activate(this);
        }


        private void Awake()
        {
            ObjectsManager = new WorkspaceObjectsManager(itemPrefab);
        }

        public void Activate(WorkspaceConfiguration wc)
        {
            Configuration = wc;

            workspaceOrigin.transform.position = wc.Origin.Position;
            workspaceOrigin.transform.rotation = wc.Origin.Rotation;

            Plane = FindObjectOfType<PlaneFactory>()
                .SpawnPlane(Configuration.Plane.Width, Configuration.Plane.Height);

            Plane.transform.SetParent(workspaceOrigin.transform);

            var spawns = Configuration.Cosmos.SpaceItems
                .Select(ci => new
                {
                    resource = Configuration.ResourceCollection.TryGetResource(ci.ResourceId),
                    ci
                })
                .Where(o => o.resource != null);

            foreach (var spawn in spawns)
            {
                ObjectsManager.SpawnItem(
                    Plane,
                    spawn.resource,
                    spawn.ci.Position,
                    spawn.ci.Rotation,
                    spawn.ci.Scale);
            }

            // FAKE CODE
            // SetUXhandler(new CompositeUXHandler(new AllowUserToPositionPlane()));
            SetUXhandler(new CompositeUxHandler(new AllowUserToSelectObjects()));
        }
    }
}