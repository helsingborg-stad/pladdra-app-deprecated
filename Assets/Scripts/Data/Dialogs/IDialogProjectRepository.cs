using System.Threading.Tasks;

namespace pladdra_app.Assets.Scripts.Data.Dialogs
{
    public interface IDialogProjectRepository {
        IWorkspaceRepository GetWorkspaceRepository();
        Task<DialogProject> Load ();
    }
}
