using System;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.Events;

public class EffectController : MonoBehaviour
{
    private List<OnExplodePrefab> activeExploadableObjects;

    private void Awake()
    {
        activeExploadableObjects = new List<OnExplodePrefab>();

        MessageBus.OnObjectExploded += OnObjectExploded;
    }

    private void OnObjectExploded(ExplodableObject obj)
    {
        OnExplodePrefab explodableObject = Instantiate(obj.ExplodePrefab);
        explodableObject.transform.position = obj.transform.position;
        explodableObject.gameObject.SetActive(true);
        explodableObject.Play();

        explodableObject.OnDestroy += OnExplodableFinished;
    }

    private void OnExplodableFinished(OnExplodePrefab obj)
    {
        obj.OnDestroy -= OnExplodableFinished;
        activeExploadableObjects.Remove(obj);
        Destroy(obj.gameObject);
    }
}