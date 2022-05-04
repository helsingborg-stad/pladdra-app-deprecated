using System.Collections.Generic;
using System.Linq;
using pladdra_app.Assets.Scripts.Entities;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.ExampleScreens
{
    public class WorkspaceScreen: Screen {
        public List<IPladdraObject> PladdraObjects { get; private set; }

        public void SetPladdraObjects(IEnumerable<IPladdraObject> objects) {
            PladdraObjects = (objects ?? Enumerable.Empty<IPladdraObject>()).ToList();
        }

        private void Start() {
            foreach(var po in PladdraObjects) {
                // po.RequestGameObject(this, (go, err) => Debug.Log("GO DONE"));
                StartCoroutine(po.CreateLoadCoRoutine((go, err) => {
                    Debug.Log("GO DONE");
                }));
            }
        }
    }
}