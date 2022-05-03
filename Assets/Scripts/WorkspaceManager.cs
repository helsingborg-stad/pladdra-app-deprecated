using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

interface ISerializeGameObjectFactory<T>
{
    T Serialize(GameObject go);
}

public class SerializeWorkspaceItemFactory : ISerializeGameObjectFactory<SerializedWorkspaceItem>
{
    public SerializedWorkspaceItem Serialize(GameObject go)
    {
        var obj = new SerializedWorkspaceItem();
        obj.id = go.name + " -- (InstanceID: " + go.GetInstanceID().ToString() + ")";
        obj.position = go.transform.localPosition;
        obj.rotation = go.transform.localRotation;
        obj.scale = go.transform.localScale;

        return obj;
    }
}

public class SerializedWorkspaceItem
{
    public string id;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

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

    public void RenderItems()
    {

    }

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



#if UNITY_EDITOR

[UnityEditor.CustomEditor(typeof(WorkspaceManager))]
public class WorkspaceManagerEditor : UnityEditor.Editor
{
    private WorkspaceManager script;

    private string json;

    private ISerializeGameObjectFactory<SerializedWorkspaceItem> serializeFactory = new SerializeWorkspaceItemFactory();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Create item"))
        {
            script.RenderItem(script.itemPrefab, Vector3.zero, new Quaternion(0, 0, 0, 1), new Vector3(1, 1, 1));
        }

        if (GUILayout.Button("Reset"))
        {
            script.DestroyItems();
        }

        json = GUILayout.TextArea(json, new[] {
            GUILayout.ExpandHeight(true)
            // GUILayout.MaxHeight(100),
            // GUILayout.MinHeight(20),
        });

        if (GUILayout.Button("DumpToJson"))
        {
            var serializedWorkspaceItems = script.QueryItems().Select(go => serializeFactory.Serialize(go));
            json = JsonConvert.SerializeObject(serializedWorkspaceItems, Newtonsoft.Json.Formatting.Indented);
        }
    }

    private void OnEnable()
    {
        script = target as WorkspaceManager;
    }
}


#endif