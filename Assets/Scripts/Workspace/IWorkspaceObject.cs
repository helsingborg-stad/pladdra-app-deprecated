using System.Numerics;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public interface IWorkspaceObject
    {
        IWorkspaceResource workspaceResource { get; }
        GameObject gameObject { get; }
    }
}