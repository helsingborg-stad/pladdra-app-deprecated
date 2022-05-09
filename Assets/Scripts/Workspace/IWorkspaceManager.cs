using pladdra_app.Assets.Scripts.Pipelines;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public interface IWorkspaceManager
    {
        IWorkspaceResourceCollection resourceCollection { get; set; }
        IWorkspaceObjectsManager objectsManager { get; }
    }
}