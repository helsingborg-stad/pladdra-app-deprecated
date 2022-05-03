using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using pladdra_app.Assets.Scripts.Data;
using pladdra_app.Assets.Scripts.Data.Dialogs;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.ExampleScreens
{

    public class LoadProjectsScreen: Screen {
        public IDialogProjectRepository Repository { get; set; }

        public LoadProjectsScreen(IDialogProjectRepository repository)
        {
            Repository = repository;
        }

        public LoadProjectsScreen(): this(new SampleDialogProjectRepository())
        {
        }

        private void Start() {
            StartCoroutine(LoadProjectAndResources());
        }

        private IEnumerator LoadProjectAndResources() {
            var wrm = new WebResourceManager();
            var task = Task.Run(async () => {
                var project = await Repository.Load();

                var paths = await wrm.GetResourcePaths(project.Resources.Select(resource => resource.Url));
                return paths;
            });

            while (!task.IsCompleted) {
                yield return new WaitForSeconds(1);
            }

            foreach(var kv in task.Result) {
                Debug.Log($"{kv.Key} => {kv.Value}");
            }

            GetComponentInParent<ScreenManager>().SetActiveScreen<WorkspaceScreen>();
        }

    }
}