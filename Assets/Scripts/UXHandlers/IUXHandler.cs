using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lean.Common;
using Lean.Touch;
using pladdra_app.Assets.Scripts.Workspace;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.UXHandlers
{
    public interface IUXHandler
    {
        void Activate(IWorkspaceScene scene);
        void Deactivate(IWorkspaceScene scene);
    }

    public class CompositeUXHandler : IUXHandler
    {
        public CompositeUXHandler(params IUXHandler[] handlers)
        {
            this.handlers = handlers;
        }

        public IUXHandler[] handlers { get; set; }

        public void Activate(IWorkspaceScene scene)
        {
            foreach (var handler in handlers)
            {
                handler.Activate(scene);
            }
        }

        public void Deactivate(IWorkspaceScene scene)
        {
            foreach (var handler in handlers)
            {
                handler.Deactivate(scene);
            }
        }
    }

    public class NullUXHandler : IUXHandler
    {
        public void Activate(IWorkspaceScene scene)
        {
        }

        public void Deactivate(IWorkspaceScene scene)
        {
        }
    }

    public abstract class AbstractLeanSelectable : IUXHandler
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
    public class AllowUserToPositionPlane : AbstractLeanSelectable
    {
        protected override IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene)
        {
            return new[] { scene.plane };
        }
    }
    public class AllowUserToSelectObjects : AbstractLeanSelectable
    {
        protected override IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene)
        {
            return scene.objectsManager.objects.Select(o => o.gameObject);
        }
    }
}