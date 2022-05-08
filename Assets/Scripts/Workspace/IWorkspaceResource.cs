using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public interface IWorkspaceResource
    {
        string resourceID { get; set; }
        GameObject prefab { get; }
    }
}