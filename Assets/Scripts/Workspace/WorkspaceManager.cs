using System.Linq;
using DefaultNamespace;
using Lean.Common;
using UnityEngine;
using UnityEngine.UIElements;
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

            SetModeAllowUserToSelectItems();
        }
        
        
        public void SetModeAllowUserToSelectItems()
        {
            SetUXhandler(new AllowUserToSelectObjects(go =>
            {
                FindObjectOfType<HudManager>()
                    .ViewFromTemplate("user-has-selected-workspace-item-hud", root =>
                    {
                        root.Q<Button>("remove").clicked += () =>
                        {
                            ObjectsManager.DestroyItem(go);
                            SetModeAllowUserToSelectItems();
                        };
                        root.Q<Button>("done").clicked += () =>
                        {
                            go.GetComponent<LeanSelectable>().Deselect();
                        };
                    });
            }, go =>
            {
                SetModeAllowUserToSelectItems();
            }));
            
            FindObjectOfType<HudManager>()
                .ViewFromTemplate("user-can-select-workspace-items-hud", root =>
                {
                    root.Q<Button>("inventory").clicked += () => { };
                    root.Q<Button>("edit-plane").clicked += () => { };
                });
        }
    }
}