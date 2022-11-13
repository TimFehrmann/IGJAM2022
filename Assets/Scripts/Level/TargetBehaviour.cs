using System;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour, IPlaceableBehaviour
{
    public event Action<TargetBehaviour> OnDestroy;

    public void Despawn()
    {
        if (OnDestroy != null)
        {
            OnDestroy(this);
        }
    }

    public bool IsPlaced()
    {
        return true;
    }

    public void OnPlacementUpdate()
    {
        //empty
    }

    public void Place()
    {
        MessageBus.SendObjectCreatedMessage(this);
    }
}