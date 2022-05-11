using System;
using System.Collections.Generic;
using UnityEngine;
using Workspace;

namespace UXHandlers
{
    public class AllowUserToPositionPlane : AbstractLeanSelectable
    {
        public AllowUserToPositionPlane(Action<GameObject> onSelected, Action<GameObject> onDeselected): base(onSelected, onDeselected)
        {
        }
        protected override IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene)
        {
            return new[] { scene.Plane };
        }
    }
}