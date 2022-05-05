using System;
using System.Collections;
using System.Collections.Generic;
using pladdra_app.Assets.Scripts.Entities;
using UnityEngine;

namespace pladdra_app.Assets.Scripts
{
    public interface IResourceManager
    {
        void PreloadResources(List<IPladdraObject> resources, Action callback);
        void UnloadAllResources();
        GameObject GetResource(string id);
        Dictionary<string, GameObject> ListResources();
    }
}