using System;
using System.Collections.Generic;
using Lean.Common;
using Lean.Touch;
using UnityEngine;
using Workspace;

namespace UXHandlers
{
    public abstract class AbstractLeanSelectable : IUxHandler
    {
        protected abstract IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene);

        protected Action<GameObject> OnSelected { get; set; }
        protected Action<GameObject> OnDeselected { get; set; }

        protected AbstractLeanSelectable(Action<GameObject> onSelected, Action<GameObject> onDeselected)
        {
            OnSelected = onSelected;
            OnDeselected = onDeselected;
        }
        
        public void Activate(IWorkspaceScene scene)
        {
            foreach (var obj in GetSelectableObjects(scene))
            {
                obj.GetComponent<LeanDragTranslateAlong>().enabled = true;
                obj.GetComponent<LeanTwistRotateAxis>().enabled = true;
                obj.GetComponent<BoxCollider>().enabled = true;
                obj.GetComponent<FlexibleBoxCollider>().SetBoxColliderSize();

                var selectable = obj.GetComponent<LeanSelectable>();
                selectable.enabled = true;
                selectable.OnSelected.AddListener(() => OnSelected(obj));
                selectable.OnDeselected.AddListener(() => OnDeselected(obj));
            }
        }

        public void Deactivate(IWorkspaceScene scene)
        {
            foreach (var obj in GetSelectableObjects(scene))
            {
                obj.GetComponent<LeanDragTranslateAlong>().enabled = false;
                obj.GetComponent<LeanTwistRotateAxis>().enabled = false;
                obj.GetComponent<BoxCollider>().enabled = false;

                var selectable = obj.GetComponent<LeanSelectable>();
                selectable.enabled = false;
                selectable.OnSelected.RemoveAllListeners();
                selectable.OnDeselected.RemoveAllListeners();
            }
        }
    }
}