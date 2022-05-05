using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pladdra_app.Assets.Scripts
{
    public class WorkspaceObject : MonoBehaviour
    {
        [field: SerializeField]
        public string id { get; private set; }
    }
}