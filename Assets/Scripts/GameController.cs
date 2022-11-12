using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public event Action<bool> OnPauseStateChanged;

    [Header("Scene references")]
    [SerializeField]
    private EnemyController enemyController;
    [SerializeField]
    private Income incomeController;
    [SerializeField]
    private PlacementSystem placementSystem;


    private bool isPaused = true;

    public bool IsPaused { 
        get => isPaused; 
        private set { 
            isPaused = value;
            if (OnPauseStateChanged != null)
            {
                OnPauseStateChanged(isPaused);
            }
        } }

    void Update()
    {
        if(!IsPaused)
        {
            enemyController.OnUpdate();
            incomeController.OnUpdate();
            placementSystem.OnUpdate();
        }
    }

    public void OnStartButtonPressed()
    {
        enemyController.SpawnNewWave();
        IsPaused = false;
    }

    public void OnTogglePause()
    {
        IsPaused = !IsPaused;
    }
}