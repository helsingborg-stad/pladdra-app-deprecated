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

    public class LoadProjectsScreen : Screen
    {
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
        private void Start()
        {
            var pipeline = new Pipeline()
            {
                CreateDialogProjectRepository = () => new SampleDialogProjectRepository(Application.temporaryCachePath),
            };

            StartCoroutine(pipeline.LoadWorkspace(wrc =>
            {
                GetComponentInParent<ScreenManager>().SetActiveScreen<WorkspaceScreen>(
                beforeActivate: screen => screen.SetWorkspace(wrc)
                );
            }));
        }
    }
}