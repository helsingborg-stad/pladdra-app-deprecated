using System;
using System.Collections;
using UnityEngine;

namespace pladdra_app.Assets.Scripts.Entities
{
    public interface IPladdraObject
    {
        IEnumerator CreateLoadCoRoutine(Action<GameObject, Exception> callback);
    }
}