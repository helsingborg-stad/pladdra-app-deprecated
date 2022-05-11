using System.Collections.Generic;
using System.Linq;
using Lean.Common;
using UnityEngine;
using UnityEngine.UIElements;
using UXHandlers;

namespace Workspace
{
    public class AllowUserToSpawnItemFromResource : AbstractUxHandler
    {
        protected override IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene)
        {
            return Enumerable.Empty<GameObject>();
        }

        public override void Activate(IWorkspaceScene scene)
        {
            base.Activate(scene);

            scene.UseHud("user-can-chose-to-spawn-item-from-resource-hud", root =>
            {
                root.Q<Button>("close").clicked += () =>
                {
                    scene.UseUxHandler(new AllowUserToPositionObjects());
                };
            });
            
        }
    }
    
    public class AllowUserToPositionObjects: AbstractUxHandler
    {
        protected override void OnSelected(IWorkspaceScene scene, GameObject go)
        {
            scene.UseHud("user-has-selected-workspace-item-hud", root =>
            {
                root.Q<Button>("remove").clicked += () =>
                {
                    scene.ObjectsManager.DestroyItem(go);
                    scene.UseUxHandler(new AllowUserToPositionObjects());
                };
                root.Q<Button>("done").clicked += () =>
                {
                    go.GetComponent<LeanSelectable>().Deselect();
                };
            });
        }

        protected override void OnDeselected(IWorkspaceScene scene, GameObject go)
        {
            scene.UseUxHandler(new AllowUserToPositionObjects());
        }

        public override void Activate(IWorkspaceScene scene)
        {
            base.Activate(scene);
            scene.UseHud("user-can-chose-workspace-action-hud", root =>
            {
                root.Q<Button>("edit-plane").clicked += () =>
                {
                    scene.UseUxHandler(new AllowUserToPositionPlane());
                };
                root.Q<Button>("inventory").clicked += () =>
                {
                    scene.UseUxHandler(new AllowUserToSpawnItemFromResource());
                };
            });
        }

        protected override IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene)
        {
            return scene.ObjectsManager.Objects.Select(o => o.GameObject);
        }
    }
}