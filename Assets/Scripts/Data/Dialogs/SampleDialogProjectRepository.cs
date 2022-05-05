using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Data.Dialogs
{
    public class SampleDialogProjectRepository: IDialogProjectRepository, IWorkspaceRepository {
        
        static string[] SampleModels = new string[] {
            // "https://modul-test.helsingborg.io/wp-content/uploads/sites/30/2022/03/telefonare.glb",
            "https://modul-test.helsingborg.io/wp-content/uploads/sites/30/2022/03/lego_test.glb",
            "https://modul-test.helsingborg.io/wp-content/uploads/sites/30/2022/02/svensgardsskolan_4.glb"
        };

        public SampleDialogProjectRepository(string tempPath)
        {
            TempPath = tempPath;
        }

        public string TempPath { get; }

        public IEnumerable<IWorkspace> GetAll(string dialogProjectId)
        {
            return Directory.EnumerateFiles(TempPath, "*.workspace.json")
                .Select(path => new Workspace());
        }

        public IWorkspaceRepository GetWorkspaceRepository()
        {
            return this;
        }

        public Task<DialogProject> Load() => Task.FromResult(new DialogProject() {
                Id = "dialog-1",
                Resources = SampleModels.Select(url => new DialogResource{Url = url, Type = "model"}).ToList()
        });
    }
}
