using System.Collections.Generic;

namespace pladdra_app.Assets.Scripts.Data.Dialogs
{
    public interface IWorkspaceRepository {
        IEnumerable<IWorkspace> GetAll(string dialogProjectId);
    }
}
