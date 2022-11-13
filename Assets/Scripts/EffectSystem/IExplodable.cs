using System;
using UnityEngine;

    public interface IExplodable
    {
        public Action<MonoBehaviour> OnDestroy { get; set; }
    }
