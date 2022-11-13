using System;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    private void Awake()
    {
        var exploadable = transform.GetComponent<IExplodable>();
        if (exploadable != null)
        {
            exploadable.OnDestroy += OnExplode;
        }
    }

    private void OnExplode(MonoBehaviour obj)
    {
        MessageBus.SendObjectExplodeMessage(this);
    }
}