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
        public WorkspaceConfiguration configuration { get; set; }

        private void Awake()
        {
            objectsManager = new WorkspaceObjectsManager(itemPrefab, targetParent);
        }

        public void Activate(WorkspaceConfiguration wc)
        {
            configuration = wc;

            var spawns = configuration.cosmos.spaceItems
                .Select(ci => new
                {
                    resource = configuration.resourceCollection.TryGetResource(ci.resourceId),
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