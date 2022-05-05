using UnityEngine;
using System;
using pladdra_app.Assets.Scripts.Data.Dialogs;
using pladdra_app.Assets.Scripts;

namespace pladdra_app.Assets.Scripts
{
    public class WorkspaceResource : IWorkspaceResource, ISerializable<DialogResource>
    {
        public string id { get; set; }
        private ResourceManagerBase resourceManager => GameObject.FindObjectOfType<ResourceManagerBase>();
        public GameObject prefab => resourceManager.GetResource(id);
        public DialogResource Serialize()
        {
            throw new NotImplementedException();
        }
    }
}

