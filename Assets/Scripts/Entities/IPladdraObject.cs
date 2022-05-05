using System;
using System.Collections;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Entities
{
    public interface IPladdraObject
    {
        string id { get; }
        IEnumerator CreateLoadCoRoutine(Action<GameObject, Exception> callback);
    }
}