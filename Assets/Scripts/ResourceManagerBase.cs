using System;
using System.Collections.Generic;
using pladdra_app.Assets.Scripts.Entities;
using UnityEngine;

public abstract class ResourceManagerBase : MonoBehaviour, IResourceManager
{
    public abstract void PreloadResources(List<IPladdraObject> resources, Action callback);
    public abstract void UnloadAllResources();
    public abstract GameObject GetResource(string id);
    public abstract Dictionary<string, GameObject> ListResources();
}
