using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public interface IWorkspaceManager
    {
        IWorkspaceResourceManager resources { get; }
        IWorkspaceObjectsManager items { get; }

        void Initialize();
        void Reset();
    }
}