using System;
using UnityEngine;
using TMPro;

public class LevelItem : MonoBehaviour
{

    public event Action<LevelItem> onDestruction;

    public int TimeToDespawn = 10;

    public float TimeRemaining { get => timeRemainingInSeconds; private set => timeRemainingInSeconds = value; }

    [SerializeField] private TextMeshPro timerText;

    // Cached
    private float timeRemainingInSeconds;


    public void Place()
    {
        timeRemainingInSeconds = TimeToDespawn;
        UpdateTimerText();
    }

    public void Despawn()
    {
        onDestruction(this);
        Destroy(gameObject);
    }

    public void SubtractTime(float deltaTime)
    {
        timeRemainingInSeconds -= deltaTime;

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
