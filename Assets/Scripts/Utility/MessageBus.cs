using System;
using UnityEngine;

public static class MessageBus
{
    public static event Action<MonoBehaviour> OnObjectCreated;

    public static void SendObjectCreatedMessage(MonoBehaviour objectCreated)
    {
        if (OnObjectCreated != null)
        {
            OnObjectCreated(objectCreated);
        }
    }

}