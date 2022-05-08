using System.Collections.Generic;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public interface IWorkspaceResourceManager
    {
        void Load(List<IWorkspaceResource> resource);
        void Load(IWorkspaceResource resource);
        void Unload(string resourceId);
        void UnloadAll();
        IWorkspaceResource Get(string resourceId);
        List<IWorkspaceResource> List();
    }
}