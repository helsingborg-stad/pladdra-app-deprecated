using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using pladdra_app.Assets.Scripts.Data;
using pladdra_app.Assets.Scripts.Data.Dialogs;
using pladdra_app.Assets.Scripts.Entities;
using pladdra_app.Assets.Scripts.Entities;
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

                foreach(var kv in paths) {
                    Debug.Log($"{kv.Key} => {kv.Value}");
                }

                var pladdraObjects = project.Resources
                    .Where(resource => paths.ContainsKey(resource.Url))
                    .Select(resource => new Pladdra3dModel(paths[resource.Url]))
                    .ToArray();

                return pladdraObjects;
            });

            while (!task.IsCompleted) {
                yield return new WaitForSeconds(1);
            }


            GetComponentInParent<ScreenManager>().SetActiveScreen<WorkspaceScreen>(
                beforeActivate: screen => screen.SetPladdraObjects(task.Result)
            );
        }

    }
}