using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Workspace;

namespace UXHandlers
{
    public class AllowUserToSelectObjects : AbstractLeanSelectable
    {
        public AllowUserToSelectObjects(Action<GameObject> onSelected, Action<GameObject> onDeselected): base(onSelected, onDeselected)
        {
        }
        protected override IEnumerable<GameObject> GetSelectableObjects(IWorkspaceScene scene)
        {
            return scene.ObjectsManager.Objects.Select(o => o.GameObject);
        }
    }
}