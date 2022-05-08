using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public class WorkspaceObject : MonoBehaviour, IWorkspaceObject
    {
        public IWorkspaceResource workspaceResource { get; private set; }
    }
}