using System;
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
                var items = scene.Resources.Resources.ToList();
                
                var listItem = Resources.Load<VisualTreeAsset>("resource-gallery-list-item-template");

                
                // The "makeItem" function will be called as needed
                // when the ListView needs more items to render
                Func<VisualElement> makeItem = () => listItem.Instantiate();

                // As the user scrolls through the list, the ListView object
                // will recycle elements created by the "makeItem"
                // and invoke the "bindItem" callback to associate
                // the element with the matching data item (specified as an index in the list)
                Action<VisualElement, int> bindItem = (e, i) =>
                {
                    var tc = e as TemplateContainer;
                    tc.Q<Label>("label").text = items[i].ResourceID;
                    tc.Q<Button>("add").clicked += () =>
                    {
                        scene.ObjectsManager.SpawnItem(scene.Plane, items[i],
                            Vector3.zero, Quaternion.identity, new Vector3(1, 1, 1));
                        scene.UseUxHandler(new AllowUserToPositionObjects());
                    };
                };
                
                var listView = root.Q<ListView>();
                listView.selectionType = SelectionType.None;
                listView.itemsSource = items;
                listView.makeItem = makeItem;
                listView.bindItem = bindItem;
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