using System;
using System.Linq;
using pladdra_app.Assets.Scripts.Pipelines;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{

    public class WorkspaceManager : MonoBehaviour, IWorkspaceManager
    {
        public GameObject itemPrefab;
        public GameObject targetParent;
        public IWorkspaceObjectsManager objectsManager { get; private set; }
        public IWorkspaceResourceCollection resourceCollection { get; set; }
        public IWorkspaceCosmos cosmos { get; private set; }

        private void Awake()
        {
            objectsManager = new WorkspaceObjectsManager(itemPrefab, targetParent);
        }

        public void Activate(
            IWorkspaceResourceCollection wrc,
            IWorkspaceCosmos cosmos)
        {
            resourceCollection = wrc;
            this.cosmos = cosmos;


            var spawns = cosmos.spaceItems
                .Select(ci => new
                {
                    resource = wrc.TryGetResource(ci.resourceId),
                    ci
                })
                .Where(o => o.resource != null);

            foreach (var spawn in spawns)
            {
                objectsManager.SpawnItem(
                    spawn.resource,
                    spawn.ci.position,
                    spawn.ci.rotation,
                    spawn.ci.scale);
            }
        }
    }
}