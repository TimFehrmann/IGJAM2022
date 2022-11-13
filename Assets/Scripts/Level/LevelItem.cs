using System;
using UnityEngine;
using TMPro;

public class LevelItem : MonoBehaviour
{
    public event Action<LevelItem> onDestruction;

    public int TimeToDespawn = 10;

    public float TimeRemaining { get => timeRemainingInSeconds; private set => timeRemainingInSeconds = value; }
    public ITEMTYPE ItemType { get => itemType; }

    [SerializeField]
    private ITEMTYPE itemType;

    [SerializeField] private TextMeshPro timerText;

    private IPlaceableBehaviour[] placeableBehaviours;
    private PlacementBlocker placementBlocker;

    // Cached
    private float timeRemainingInSeconds;

    private void Awake()
    {
        placeableBehaviours = GetComponentsInChildren<IPlaceableBehaviour>();
        placementBlocker = GetComponentInChildren<PlacementBlocker>();
        Debug.Log(placeableBehaviours);
    }

    public void Place()
    {
        timeRemainingInSeconds = TimeToDespawn;
        UpdateTimerText();
        foreach (var behaviour in placeableBehaviours)
        {
            behaviour.Place();
        }

        // Enable Placement Blockers
        if (placementBlocker != null)
        {
            placementBlocker.Activate();
        }

    }

    public void Despawn()
    {
        foreach (var behaviour in placeableBehaviours)
        {
            behaviour.Despawn();
        }

        // Enable Placement Blockers
        if (placementBlocker != null)
        {
            placementBlocker.Deactivate();
        }

        onDestruction(this);
        Destroy(gameObject);
    }

    public void SubtractTime(float deltaTime)
    {
        bool readyForCD = true;
        foreach (var behaviour in placeableBehaviours)
        {
            behaviour.OnPlacementUpdate();
            if (!behaviour.IsPlaced())
            {
                readyForCD = false;
            }
        }

        if (readyForCD)
        {
            timeRemainingInSeconds -= deltaTime;
        }

        bool outOfTime = timeRemainingInSeconds <= 0;
        if (outOfTime)
        {
            Despawn();
        }
        else
        {
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = GetSecondsRemaining().ToString();
    }

    private int GetSecondsRemaining()
    {
        return Mathf.RoundToInt(timeRemainingInSeconds);
    }
}
