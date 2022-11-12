using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Prefab references")]
    [SerializeField]
    private Enemy enemyPrefab;

    [Header("Scene references")]
    [SerializeField]
    private Transform enemySpawn;

    [Header("Settings")]
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private int defaultWaveSize;
    [SerializeField]
    [Range(0f, 1f)]
    private float newWaveStartTrigger;
    [SerializeField]
    private int sizeIncreasePerWave;

    [Tooltip("Start movedirection of enemies, 1st enemy runs in [0] direction, 2nd in [1] and so on; cycles.")]
    [SerializeField]
    private DIRECTION[] spawnDirections;

    private int currentSpawnDirectionIndex = 0;
    private int currentWaveSize;

    private ObjectPool<Enemy> enemyPool;

    private List<Enemy> activeEnemies = new List<Enemy>();


    private void Start()
    {
        enemyPool = new ObjectPool<Enemy>(6, enemyPrefab);
        currentWaveSize = defaultWaveSize;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            activeEnemies[i].UpdateMovement();
        }
       
    }

    public void SpawnNewWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentWaveSize; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = enemyPool.GetObject();
        enemy.transform.position = enemySpawn.position;
        enemy.OnDestruction += DespawnEnemy;

        DIRECTION spawnDirection = spawnDirections[currentSpawnDirectionIndex];
        enemy.SetClockwiseDirection(spawnDirection);
        enemy.SetMoveDirection(spawnDirection);
        currentSpawnDirectionIndex = currentSpawnDirectionIndex + 1 >= spawnDirections.Length ? currentSpawnDirectionIndex = 0 : currentSpawnDirectionIndex + 1;

        enemy.gameObject.SetActive(true);
        activeEnemies.Add(enemy);
    }

    private void DespawnEnemy(Enemy enemyToDespawn)
    {
        enemyToDespawn.OnDestruction -= DespawnEnemy;
        activeEnemies.Remove(enemyToDespawn);
        enemyPool.ReturnToPool(enemyToDespawn);
        if (activeEnemies.Count <= currentWaveSize * newWaveStartTrigger)
        {
            currentWaveSize += sizeIncreasePerWave;
            SpawnNewWave();
        }
    }
}
