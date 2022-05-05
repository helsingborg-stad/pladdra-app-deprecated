using System.Collections.Generic;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Data.Dialogs
{
    public class DialogWorkspaceItem
    {
        public string id;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public DialogResource resource;
    }

    public class DialogResource
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
    public class DialogProject
    {
        public string Id { get; set; }

        public List<DialogResource> Resources { get; set; }
    }
}
