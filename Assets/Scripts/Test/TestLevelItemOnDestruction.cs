using UnityEngine;

public class TestLevelItemOnDestruction : MonoBehaviour
{
    // Cached
    private PlacementSystem placementSystem;

    private void Awake()
    {
        placementSystem = FindObjectOfType<PlacementSystem>();
        placementSystem.OnDestruction += LevelItemOnDestruction;
    }

    private void LevelItemOnDestruction(LevelItem levelItem, int amount)
    {
     //   Debug.Log("Event: LevelItemOnDestruction - " + levelItem.name);
    }
}
