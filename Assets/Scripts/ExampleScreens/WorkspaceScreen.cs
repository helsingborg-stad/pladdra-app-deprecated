using System.Collections.Generic;
using System.Linq;
using pladdra_app.Assets.Scripts.Entities;
using pladdra_app.Assets.Scripts.Pipelines;
using pladdra_app.Assets.Scripts.Workspace;
using UnityEditor;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.ExampleScreens
{
    public class WorkspaceScreen : Screen
    {
        public IWorkspaceManager workspace;

        public IWorkspaceResourceCollection WorkspaceResourceCollection { get; private set; }

        private void Start()
        {
        }

        public void SetWorkspace(IWorkspaceResourceCollection wrc)
        {
            WorkspaceResourceCollection = wrc;
        }

        protected override void BeforeActivateScreen()
        {
            workspace = UnityEngine.Object.FindObjectOfType<WorkspaceManager>();
            workspace.resources.Load(WorkspaceResourceCollection.resources.ToList());
        }


        protected override void AfterActivateScreen()
        {
            // TODO: Given resources and possibly  3d space configutrations fomr items,
            var i = 0;
            foreach (var resource in workspace.resources.List())
            {
                workspace.items.SpawnItem(resource, new Vector3(i, 0, 0), new Quaternion(), new Vector3(1, 1, 1));
                i = i + 3;
            }
        }
    }
}