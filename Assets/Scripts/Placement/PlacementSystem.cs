using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public event Action<LevelItem, int> OnDestruction;
    public event Action<LevelItem, int> OnItemPlaced;

    // Cached
    private List<LevelItem> levelItems;
    private List<LevelItem> levelItemsToRemove;

    public void RegisterLevelItem(LevelItem levelItem)
    {
        levelItems.Add(levelItem);
        levelItem.Place();
        levelItem.onDestruction += DeregisterLevelItem;

        int sameItemsCount = levelItems.Count(item => item.ItemType == levelItem.ItemType);
        if (OnItemPlaced != null)
        {
            OnItemPlaced(levelItem, sameItemsCount);
        }
    }

    public void DeregisterLevelItem(LevelItem levelItem)
    {
        levelItemsToRemove.Add(levelItem);
        levelItem.onDestruction -= DeregisterLevelItem;
        int sameItemsCount = levelItems.Count(item => item.ItemType == levelItem.ItemType) - 1;
        if (OnDestruction != null)
        {
            OnDestruction(levelItem, sameItemsCount);
        }
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
        foreach (LevelItem levelItem in levelItems)
        {
            levelItem.SubtractTime(deltaTime);
        }
    }
}
