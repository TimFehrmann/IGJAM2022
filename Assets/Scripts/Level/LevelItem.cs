using System;
using UnityEngine;
using TMPro;

public class LevelItem : MonoBehaviour
{

    public event Action<LevelItem> onDestruction;

    public int TimeToDespawn = 10;

    public float TimeRemaining { get => timeRemainingInSeconds; private set => timeRemainingInSeconds = value; }

    [SerializeField] private TextMeshPro timerText;

    private IPlaceableBehaviour[] placeableBehaviours;

    // Cached
    private float timeRemainingInSeconds;

    private void Awake()
    {
        placeableBehaviours = GetComponentsInChildren<IPlaceableBehaviour>();
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
    }

    public void Despawn()
    {
        foreach (var behaviour in placeableBehaviours)
        {
            behaviour.Despawn();
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
            if(!behaviour.IsPlaced())
            {
                readyForCD = false;
            }
        }

        if(readyForCD)
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
