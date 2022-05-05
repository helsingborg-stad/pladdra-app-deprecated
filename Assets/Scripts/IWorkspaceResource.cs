using UnityEngine;

namespace pladdra_app.Assets.Scripts
{
    public interface IWorkspaceResource
    {
        string id { get; set; }
        GameObject prefab { get; }
    }

}