using System.Collections.Generic;
using System.Linq;
using pladdra_app.Assets.Scripts.Entities;
using pladdra_app.Assets.Scripts.Pipelines;

namespace pladdra_app.Assets.Scripts.ExampleScreens
{
    public class WorkspaceScreen: Screen {
        public IPladdraWorkspace Workspace { get; private set; }

        private void Start() {
            /*
            foreach(var po in PladdraObjects) {
                // po.RequestGameObject(this, (go, err) => Debug.Log("GO DONE"));
                StartCoroutine(po.CreateLoadCoRoutine((go, err) => {
                    Debug.Log("GO DONE");
                }));
            }
            */
        }

        internal void SetWorkspace(IPladdraWorkspace ws)
        {
            Workspace = ws;
        }
    }
}