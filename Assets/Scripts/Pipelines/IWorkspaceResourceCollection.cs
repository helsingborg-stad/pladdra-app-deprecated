using System.Collections.Generic;
using System.Linq;
using pladdra_app.Assets.Scripts.Workspace;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Pipelines
{

    public class WorkspaceConfiguration
    {
        public WorkspaceOrigin origin { get; set; }
        public IWorkspacePlane plane { get; set; }
        public IWorkspaceCosmos cosmos { get; set; }
        public IWorkspaceResourceCollection resourceCollection { get; set; }
    }

    public class WorkspaceOrigin
    {
        public Vector3 position = new Vector3();
        public Quaternion rotation = new Quaternion();
    }

    public interface IWorkspacePlane
    {
        int width { get; set; }
        int height { get; set; }
    }

    public class WorkspacePlane : IWorkspacePlane
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public interface IWorkspaceCosmos
    {
        IEnumerable<IWorkspaceItemInSpace> spaceItems { get; }
    }
    public class WorkspaceCosmos : IWorkspaceCosmos
    {
        public IEnumerable<IWorkspaceItemInSpace> spaceItems { get; set; }
    }

    public interface IWorkspaceItemInSpace
    {
        public string resourceId { get; }
        public Vector3 position { get; }
        public Vector3 scale { get; }
        public Quaternion rotation { get; }
    }

    public class WorkspaceItemInSpace : IWorkspaceItemInSpace
    {
        public string resourceId { get; set; }

        public Vector3 position { get; set; }

        public Vector3 scale { get; set; }

        public Quaternion rotation { get; set; }
    }

    public interface IWorkspaceResourceCollection
    {
        IWorkspaceResource TryGetResource(string resourceId);
        IEnumerable<IWorkspaceResource> resources { get; }
    }

    public class WorkspaceResourceCollection : IWorkspaceResourceCollection
    {
        public IWorkspaceResource TryGetResource(string resourceId) => resources.FirstOrDefault(r => r.resourceID == resourceId);

        public IEnumerable<IWorkspaceResource> resources
        {
            get; set;
        }
    }
}