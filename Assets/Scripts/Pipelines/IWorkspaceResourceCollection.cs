using System.Collections.Generic;
using pladdra_app.Assets.Scripts.Workspace;

namespace pladdra_app.Assets.Scripts.Pipelines
{
    public interface IWorkspaceResourceCollection
    {
        IEnumerable<IWorkspaceResource> resources { get; }
    }

    public class WorkspaceResourceCollection : IWorkspaceResourceCollection
    {
        public IEnumerable<IWorkspaceResource> resources
        {
            get; set;
        }
    }
}