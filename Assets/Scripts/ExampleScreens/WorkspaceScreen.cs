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
        public IWorkspaceResourceCollection resourceCollection { get; private set; }
        public IWorkspaceCosmos cosmos { get; private set; }

        private void Start()
        {
        }

        public void SetWorkspace(IWorkspaceResourceCollection wrc, IWorkspaceCosmos cosmos)
        {
            resourceCollection = wrc;
            this.cosmos = cosmos;
        }

        protected override void BeforeActivateScreen()
        {
        }

        protected override void AfterActivateScreen()
        {
            FindObjectOfType<WorkspaceManager>()
                .Activate(resourceCollection, cosmos);
        }
    }
}