using System;
using System.Collections;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Entities
{
    public class Pladdra3dModel: IPladdraObject {

        public Pladdra3dModel(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }

        public IEnumerator CreateLoadCoRoutine(Action<GameObject, Exception> callback)
        {
            var options = new Piglet.GltfImportOptions(){
                AutoScale = true,
                AutoScaleSize = 1f,
                // ShowModelAfterImport = false
            };
            Tuple<GameObject, Exception> result = null;
            var task = Piglet.RuntimeGltfImporter.GetImportTask(Path, options);
            task.OnAborted = () => result = new Tuple<GameObject, Exception>(null, null);
            task.OnCompleted = go => result = new Tuple<GameObject, Exception>(go, null);
            task.OnException = err => result = new Tuple<GameObject, Exception>(null, err);

            while (result == null) {
                task.MoveNext();
                yield return null;
            }
            callback(result.Item1, result.Item2);
        }
    }
}