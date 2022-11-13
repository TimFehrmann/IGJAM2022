using System;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EffectController : MonoBehaviour
{
    [Header("Local references")] [SerializeField]
    private OnExplodePrefab projectileExplodePrefab;

    private ObjectPool<OnExplodePrefab> projectileExplodeOpjectpool;
    private List<OnExplodePrefab> activeExploadableObjects;

    private void Awake()
    {
        projectileExplodeOpjectpool = new ObjectPool<OnExplodePrefab>(10, projectileExplodePrefab);
        activeExploadableObjects = new List<OnExplodePrefab>();

        MessageBus.OnObjectExploded += OnObjectExploded;
    }

    private void OnObjectExploded(ExplodableObject obj)
    {
        OnExplodePrefab explodableObject = null;
        bool isProjectile = obj.GetComponent<Projectile>() != null;
        
        if (isProjectile)
        {
            explodableObject = projectileExplodeOpjectpool.GetObject();
        }

        if (explodableObject == null)
        {
            return;
        }
        
        explodableObject.transform.position = obj.transform.position;
        explodableObject.Init();
        explodableObject.gameObject.SetActive(true);
        explodableObject.Play();

        explodableObject.OnDestroy += OnExplodableFinished;
    }

    private void OnExplodableFinished(OnExplodePrefab obj)
    {
        obj.OnDestroy -= OnExplodableFinished;
        activeExploadableObjects.Remove(obj);
        
        //TODO NICHT ALLES ZU PROJECTILE HINZUFÜGEN!!! 
        
        projectileExplodeOpjectpool.ReturnToPool(obj);
    }
}