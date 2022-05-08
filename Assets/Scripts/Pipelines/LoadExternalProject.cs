using System;
using System.Collections.Generic;
using pladdra_app.Assets.Scripts.Data.Dialogs;
using pladdra_app.Assets.Scripts.Workspace;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Pipelines
{
    public class LoadExternalProject : TaskYieldInstruction<DialogProject>
    {
        public LoadExternalProject(IDialogProjectRepository repository, Action<DialogProject> callback) : base(() => repository.Load(), callback) { }
    }
    public interface IPladdraWorkspace
    {
        IEnumerable<IWorkspaceResource> resources { get; }
    }
    public interface IPladdraWorkspaceItem
    {
        GameObject Prefab { get; }
    }
    public class PladdraWorkspace : IPladdraWorkspace
    {
        public IEnumerable<IWorkspaceResource> resources { get; set; }
    }
    public class PladdraWorkspaceItem : IPladdraWorkspaceItem
    {
        public GameObject Prefab { get; set; }
    }
}
