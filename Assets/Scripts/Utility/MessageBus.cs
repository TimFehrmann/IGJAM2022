using System;
using UnityEngine;

public static class MessageBus
{
    public static event Action<MonoBehaviour> OnObjectCreated;
    public static event Action<ExplodableObject> OnObjectExploded;

    public static void SendObjectCreatedMessage(MonoBehaviour objectCreated)
    {
        if (OnObjectCreated != null)
        {
            OnObjectCreated(objectCreated);
        }
    }
    
    public static void SendObjectExplodeMessage(ExplodableObject explodableObject)
    {
        if (OnObjectExploded != null)
        {
            OnObjectExploded(explodableObject);
        }
    }

}