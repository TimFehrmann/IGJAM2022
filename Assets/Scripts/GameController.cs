using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private ProjectileController projectileController;
    [SerializeField] 
    private Canvas gameOverCanvas;

    private bool isPaused = true;

    private void Awake()
    {
        projectileController.OnGameOver +=OnGameOver;
    }

    private void OnGameOver()
    {
        IsPaused = true;
        gameOverCanvas.gameObject.SetActive(true);
    }

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
        projectileController.ResetProjectiles();
        enemyController.SpawnNewWave();
        IsPaused = false;
    }

    public void OnTogglePause()
    {
        IsPaused = !IsPaused;
    }

    public void OnRestart()
    {
        //Eventuell noch auf richtigen restart umbauen

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}