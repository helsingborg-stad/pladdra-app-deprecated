using System.Collections.Generic;
using System.Linq;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public class WorkspaceResourceManager : IWorkspaceResourceManager, IWorkspaceSubManager
    {
        private List<IWorkspaceResource> _resources = new List<IWorkspaceResource>();

        public IWorkspaceManager workspace { get; private set; }

        public WorkspaceResourceManager(IWorkspaceManager workspaceManager)
        {
            workspace = workspaceManager;
        }

        public void Load(List<IWorkspaceResource> resources) => resources.ForEach(Load);

        public void Load(IWorkspaceResource resource) => _resources.Add(resource);

        public void Unload(string resourceId)
        {
            UnityEngine.Object.Destroy(Get(resourceId).prefab);
            _resources = _resources.Where(r => r.resourceID != resourceId).ToList();
        }

        public void UnloadAll() => _resources.Select(resource => resource.resourceID).ToList().ForEach(Unload);

        public IWorkspaceResource Get(string resourceId) => _resources.Find(resource => resource.resourceID == resourceId);

        public List<IWorkspaceResource> List() => _resources;
    }
}