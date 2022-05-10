using Data.Dialogs;
using Pipelines;
using Screens;
using UnityEngine;
using Screen = Screens.Screen;

namespace ExampleScreens
{

    public class LoadProjectsScreen : Screen
    {
        private void Start()
        {
            var pipeline = new Pipeline()
            {
                CreateDialogProjectRepository = () => new SampleDialogProjectRepository(Application.temporaryCachePath),
            };

            StartCoroutine(pipeline.LoadWorkspace((configuration) =>
            {
                GetComponentInParent<ScreenManager>().SetActiveScreen<WorkspaceScreen>(
                    beforeActivate: screen => screen.SetWorkspaceConfiguration(configuration)
                );
            }));
        }
    }
}