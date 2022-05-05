using System;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Pipelines
{
    public class Load3dModel: CustomYieldInstruction {
        public Piglet.GltfImportTask Task { get; }
        public Tuple<GameObject, Exception> Result {get; private set; }

        public Load3dModel(string path, Action<GameObject> callback) {
            Callback = callback;
            var options = new Piglet.GltfImportOptions(){
                AutoScale = true,
                AutoScaleSize = 1f,
                ShowModelAfterImport = false
            };
            Task = Piglet.RuntimeGltfImporter.GetImportTask(path, options);
            Task.OnAborted = () => Result = new Tuple<GameObject, Exception>(null, null);
            Task.OnCompleted = go => Result = new Tuple<GameObject, Exception>(go, null);
            Task.OnException = err => Result = new Tuple<GameObject, Exception>(null, err);
        }

        public override bool keepWaiting {
            get {
                if (Result == null) {
                    Task.MoveNext();
                    return true;
                }
                if (Result.Item2 != null) {
                    throw Result.Item2;
                }
                Callback(Result.Item1);
                return false;
            }
        }

        public Action<GameObject> Callback { get; }
    }
}
