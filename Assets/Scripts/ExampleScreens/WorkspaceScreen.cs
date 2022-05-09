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
        public WorkspaceConfiguration configuration { get; private set; }

        private void Start()
        {
        }

        public void SetWorkspaceConfiguration(WorkspaceConfiguration wc)
        {
            configuration = wc;
        }

        protected override void BeforeActivateScreen()
        {
        }

        protected override void AfterActivateScreen()
        {
            FindObjectOfType<WorkspaceManager>()
                .Activate(configuration);
        }
    }
}