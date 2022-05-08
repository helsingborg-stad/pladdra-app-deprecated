using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{

    public class WorkspaceManager : MonoBehaviour, IWorkspaceManager
    {
        public GameObject itemPrefab;
        public GameObject targetParent;
        public IWorkspaceResourceManager resources { get; private set; }
        public IWorkspaceObjectsManager items { get; private set; }

        private void Awake()
        {
            resources = new WorkspaceResourceManager(this);
            items = new WorkspaceObjectsManager(itemPrefab, targetParent);
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}