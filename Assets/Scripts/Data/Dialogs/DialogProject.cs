using System.Collections.Generic;

namespace pladdra_app.Assets.Scripts.Data.Dialogs
{
    public class DialogResource {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
    public class DialogProject {
        public string Id { get; set; }

        public List<DialogResource> Resources { get; set; }
    }
}
