using System.Collections.Generic;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public interface IWorkspaceObjectsManager
    {
        IEnumerable<IWorkspaceObject> objects { get; }

        void SpawnItem(GameObject targetParent, IWorkspaceResource resource, Vector3 position, Quaternion rotation, Vector3 scale);
    }
}