using System.Collections.Generic;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public interface IWorkspaceObjectsManager
    {
        void SpawnItem(IWorkspaceResource resource, Vector3 position, Quaternion rotation, Vector3 scale);
    }
}