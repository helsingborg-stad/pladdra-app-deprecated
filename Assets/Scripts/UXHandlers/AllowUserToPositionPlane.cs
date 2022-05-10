using System.Collections.Generic;
using UnityEngine;
using Workspace;

namespace UXHandlers
{
    public class AllowUserToPositionPlane : AbstractLeanSelectable
    {
        protected override IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene)
        {
            return new[] { scene.Plane };
        }
    }
}