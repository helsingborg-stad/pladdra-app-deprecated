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

    public class AllowUserToPositionPlane : IUXHandler
    {
        public void Activate(IWorkspaceScene scene)
        {
            scene.plane.GetComponent<LeanDragTranslateAlong>().enabled = true;
            scene.plane.GetComponent<LeanTwistRotateAxis>().enabled = true;
            scene.plane.GetComponent<LeanSelectable>().enabled = true;
            scene.plane.GetComponent<LeanSelectable>().enabled = true;
            scene.plane.GetComponent<BoxCollider>().enabled = true;
            scene.plane.GetComponent<FlexibleBoxCollider>().SetBoxColliderSize();
        }

        public void Deactivate(IWorkspaceScene scene)
        {
            scene.plane.GetComponent<LeanDragTranslateAlong>().enabled = false;
            scene.plane.GetComponent<LeanTwistRotateAxis>().enabled = false;
            scene.plane.GetComponent<LeanSelectable>().enabled = false;
            scene.plane.GetComponent<BoxCollider>().enabled = false;
        }
    }
}