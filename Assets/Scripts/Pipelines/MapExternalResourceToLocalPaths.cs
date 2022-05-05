using System;
using System.Collections.Generic;
using System.Linq;
using pladdra_app.Assets.Scripts.Data;
using pladdra_app.Assets.Scripts.Data.Dialogs;

namespace pladdra_app.Assets.Scripts.Pipelines
{
    public class MapExternalResourceToLocalPaths : TaskYieldInstruction<Dictionary<string, string>>
    {
        public MapExternalResourceToLocalPaths(WebResourceManager wrm, DialogProject project, Action<Dictionary<string, string>> callback): this(
            wrm,
            project.Resources.Select(resource => resource.Url),
            callback
        )
        {
        }

        public MapExternalResourceToLocalPaths(WebResourceManager wrm, IEnumerable<string> urls, Action<Dictionary<string, string>> callback) : base(() => wrm.GetResourcePaths(urls), callback)
        {
        }
    }
}
