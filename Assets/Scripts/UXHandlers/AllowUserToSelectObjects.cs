using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Workspace;

namespace UXHandlers
{
    public class AllowUserToSelectObjects : AbstractLeanSelectable
    {
        protected override IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene)
        {
            return scene.ObjectsManager.Objects.Select(o => o.GameObject);
        }
    }
}