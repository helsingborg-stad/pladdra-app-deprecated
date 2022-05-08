using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public class WorkspaceResource : IWorkspaceResource
    {
        public string resourceID { get; set; }
        public GameObject prefab { get; set; }
    }
}

