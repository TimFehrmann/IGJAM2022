using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public Action<LevelItem> OnDestruction;

    // Cached
    private List<LevelItem> levelItems;
    private List<LevelItem> levelItemsToRemove;

    public void RegisterLevelItem(LevelItem levelItem)
    {
        levelItems.Add(levelItem);
        levelItem.Place();
        levelItem.onDestruction += DeregisterLevelItem;
    }

    public void DeregisterLevelItem(LevelItem levelItem)
    {
        levelItemsToRemove.Add(levelItem);
        levelItem.onDestruction -= DeregisterLevelItem;
        OnDestruction(levelItem);
    }

    private void Awake()
    {
        levelItems = new List<LevelItem>();
        levelItemsToRemove = new List<LevelItem>();
    }

    public void OnUpdate()
    {
        // Remove destroyed LevelItems
        foreach (LevelItem levelItem in levelItemsToRemove)
        {
            levelItems.Remove(levelItem);
        }

        levelItemsToRemove.Clear();

        // Update Despawn Time
        float deltaTime = Time.deltaTime;
        foreach(LevelItem levelItem in levelItems)
        {
            levelItem.SubtractTime(deltaTime);
        }
    }
}
