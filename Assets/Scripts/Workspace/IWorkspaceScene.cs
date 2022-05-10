using UnityEngine;

namespace Workspace
{
    public interface IWorkspaceScene
    {
        GameObject Plane { get; }
        IWorkspaceObjectsManager ObjectsManager { get; }
    }
}