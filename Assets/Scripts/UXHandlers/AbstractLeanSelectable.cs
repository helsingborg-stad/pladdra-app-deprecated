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
        public void Activate(IWorkspaceScene scene)
        {
            foreach (var obj in GetSelectableObjects(scene))
            {
                obj.GetComponent<LeanDragTranslateAlong>().enabled = true;
                obj.GetComponent<LeanTwistRotateAxis>().enabled = true;
                obj.GetComponent<LeanSelectable>().enabled = true;
                obj.GetComponent<BoxCollider>().enabled = true;
                obj.GetComponent<FlexibleBoxCollider>().SetBoxColliderSize();
            }
        }

        public void Deactivate(IWorkspaceScene scene)
        {
            foreach (var obj in GetSelectableObjects(scene))
            {
                obj.GetComponent<LeanDragTranslateAlong>().enabled = false;
                obj.GetComponent<LeanTwistRotateAxis>().enabled = false;
                obj.GetComponent<LeanSelectable>().enabled = false;
                obj.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}