using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pladdra_app.Assets.Scripts.Data;
using pladdra_app.Assets.Scripts.Data.Dialogs;
using pladdra_app.Assets.Scripts.Entities;
using pladdra_app.Assets.Scripts.Pipelines;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.ExampleScreens
{

    public class LoadProjectsScreen: Screen {
  /*
        public IDialogProjectRepository Repository { get; set; }

        public LoadProjectsScreen(IDialogProjectRepository repository)
        {
            Repository = repository;
        }

        public LoadProjectsScreen() : this(new SampleDialogProjectRepository(Application.temporaryCachePath))
        {
        }
*/
        private void Start() {
            /*
            IEnumerator Pipeline () {
                Debug.Log("[pipline] loading external project...");
                var repo = new SampleDialogProjectRepository(Application.temporaryCachePath);
                DialogProject project = null;
                yield return new LoadExternalProject(repo, p => project = p);
                Debug.Log("[pipline] done loading external project");

                Debug.Log("[pipline] mapping external resources to local...");
                var wrm = new WebResourceManager(Application.temporaryCachePath);
                Dictionary<string, string> url2path = null;
                yield return new MapExternalResourceToLocalPaths(wrm, project, p => url2path = p);
                Debug.Log("[pipline] done mapping external resources to local");

                Debug.Log("[pipline] loading 3d model prefabs...");
                var path2model = new Dictionary<string, GameObject>();
                foreach(var path in url2path.Values) {
                    yield return new Load3dModel(path, go => {
                        Debug.Log($"[pipline] done loading 3d model {path}");
                        path2model[path] = go;
                    });
                }
                Debug.Log("[pipline] done loading 3d model prefabs");

                Debug.Log("[pipline] creating workspace...");
                var ws = new PladdraWorkspace() {
                    Items = project.Resources
                        .Where(resource => resource.Type == "model")
                        .Select(resource => new PladdraWorkspaceItem() {
                            Prefab = path2model.TryGet(url2path.TryGet(resource.Url))
                        })
                        .Where(item => item.Prefab != null)
                        .ToList()
                };

                GetComponentInParent<ScreenManager>().SetActiveScreen<WorkspaceScreen>(
                    beforeActivate: screen => screen.SetWorkspace(ws)    
                );
            }

            StartCoroutine(Pipeline());
            */

            var pipeline = new Pipeline() {
                CreateDialogProjectRepository = () => new SampleDialogProjectRepository(Application.temporaryCachePath),
            };

            StartCoroutine(pipeline.LoadWorkspace(workspace => {
                GetComponentInParent<ScreenManager>().SetActiveScreen<WorkspaceScreen>(
                    beforeActivate: screen => screen.SetWorkspace(workspace)    
                );
            }));
        }
   }
}