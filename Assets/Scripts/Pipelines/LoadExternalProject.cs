using System;
using System.Collections.Generic;
using pladdra_app.Assets.Scripts.Data.Dialogs;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Pipelines
{
    public class LoadExternalProject : TaskYieldInstruction<DialogProject>
    {
        public LoadExternalProject(IDialogProjectRepository repository, Action<DialogProject> callback): base(() => repository.Load(), callback) {}
    }


    public interface IPladdraWorkspace {
        IEnumerable<IPladdraWorkspaceItem> Items {get; }
    }
    public interface IPladdraWorkspaceItem {
        GameObject Prefab { get; }
    }
    public class PladdraWorkspace : IPladdraWorkspace
    {
        public IEnumerable<IPladdraWorkspaceItem> Items {get; set; }
    }
    public class PladdraWorkspaceItem : IPladdraWorkspaceItem
    {
        public GameObject Prefab {get; set; }
    }
}
