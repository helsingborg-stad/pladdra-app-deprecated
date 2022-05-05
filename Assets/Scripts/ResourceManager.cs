using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphQL.NewtonsoftJson;
using pladdra_app.Assets.Scripts.Entities;
using UnityEngine;

namespace pladdra_app.Assets.Scripts
{
    public class ResourceManager : ResourceManagerBase
    {
        private Dictionary<string, GameObject> resources = new Dictionary<string, GameObject>();
        public override void PreloadResources(List<IPladdraObject> pladdraObjects, Action callback)
        {
            UnloadAllResources();

            var total = pladdraObjects.Count;
            var i = 0;
            foreach (var po in pladdraObjects)
            {
                StartCoroutine(po.CreateLoadCoRoutine((go, err) =>
                {
                    i++;
                    resources.Add(po.id, go);
                    go.transform.SetParent(gameObject.transform);

                    if (i >= total)
                        callback();

                }));
            }
        }
        public override GameObject GetResource(string key) => resources[key];
        public override Dictionary<string, GameObject> ListResources() => resources;
        public override void UnloadAllResources()
        {
            resources.Keys.ToList().ForEach(key =>
            {
                UnityEngine.Object.Destroy(resources[key]);
                resources.Remove(key);
            });
        }
    }
}