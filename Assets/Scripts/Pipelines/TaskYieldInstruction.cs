using System;
using System.Threading.Tasks;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Pipelines
{
    public class TaskYieldInstruction<T> : CustomYieldInstruction
    {
        public override bool keepWaiting {
            get {
                if (Task.IsFaulted) {
                    throw Task.Exception;
                }
                if (Task.IsCompleted) {
                    Callback(Task.Result);
                    return false;
                }
                return true;
            }
        }

        public Func<Task<T>> Action { get; }
        public Action<T> Callback { get; }
        public Task<T> Task { get; }

        public TaskYieldInstruction(Func<Task<T>> action, Action<T> callback)
        {
            Action = action;
            Callback = callback;
            Task = System.Threading.Tasks.Task.Run(action);
        }
    }
}
