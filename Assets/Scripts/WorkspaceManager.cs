using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System;
using pladdra_app.Assets.Scripts.Data.Dialogs;
using pladdra_app.Assets.Scripts;
namespace pladdra_app.Assets.Scripts
{
    public class WorkspaceManager : MonoBehaviour
    {
        public GameObject itemPrefab;
        public WorkspaceObject requiredObject;
        public GameObject targetParent;
        private System.Type _component
        {
            get
            {
                return requiredObject == null ? typeof(WorkspaceObject) : requiredObject.GetType();
            }
        }

        public GameObject[] QueryItems()
        {
            return targetParent.GetComponentsInChildren(_component)
                .Select(C => C.gameObject)
                .ToArray();
        }

        public T[] QueryItems<T>() where T : WorkspaceObject
        {
            return targetParent
                .GetComponentsInChildren<T>();
        }

        public void RenderItems(List<RenderItemProps> items) => items.ForEach(RenderItem);

        public void DestroyItems()
        {
            foreach (var item in QueryItems())
            {
                DestroyItem(item);
            }
        }

        public void DestroyItem(GameObject go)
        {
            UnityEngine.Object.Destroy(go);
        }

        public void RenderItem(RenderItemProps props) => RenderItem(props.resource.prefab, props.position, props.rotation, props.scale);

        public void RenderItem(GameObject original, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GameObject go = UnityEngine.Object.Instantiate(original);
            go.transform.SetParent(targetParent.transform);
            TransformItem(go, position, rotation, scale);
        }

        public void TransformItem(GameObject go, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            go.transform.localPosition = position;
            go.transform.localRotation = rotation;
            go.transform.localScale = scale;
        }
    }
}