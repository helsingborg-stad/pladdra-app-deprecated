using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Workspace
{
    public class WorkspaceObjectsManager : IWorkspaceSubManager, IWorkspaceObjectsManager
    {
        public class Item : IWorkspaceObject
        {
            public GameObject gameObject { get; set; }
            public WorkspaceObject workspaceObject { get; set; }
            public IWorkspaceResource workspaceResource { get; set; }
        }
        private GameObject itemPrefab;
        private GameObject targetParent;
        private List<Item> items;
        public WorkspaceObjectsManager(GameObject itemPrefab, GameObject targetParent)
        {
            this.itemPrefab = itemPrefab;
            this.targetParent = targetParent;
            items = items ?? new List<Item>();
        }

        public IWorkspaceObject GetWorkspaceObject(GameObject go) => items.Find(item => item.workspaceObject == go);
        public void SpawnItem(IWorkspaceResource resource, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GameObject go = UnityEngine.Object.Instantiate(itemPrefab, position, rotation, targetParent.transform);
            go.SetActive(false);
            go.transform.SetParent(targetParent.transform);
            TransformItem(go, position, rotation, scale);

            GameObject resourceGo = UnityEngine.Object.Instantiate(resource.prefab, position, rotation, go.transform);
            resourceGo.SetActive(true);

            items.Add(new Item
            {
                gameObject = go,
                workspaceObject = go.GetComponent<WorkspaceObject>(),
                workspaceResource = resource
            });

            go.SetActive(true);
        }

        public void TransformItem(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            go.transform.localPosition = position;
            go.transform.localRotation = rotation;
            go.transform.localScale = scale;
        }
    }
}