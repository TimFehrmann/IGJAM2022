using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Prefab references")]
    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private float spawnInterval;

    [SerializeField]
    private int defaultWaveSize;

    private ObjectPool<Enemy> enemyPool;

    private List<Enemy> activeEnemies;


    private IEnumerator Start()
    {
        for (int i = 0; i < defaultWaveSize; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    public void OnUpdate()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            activeEnemies[i].UpdateMovement();
        }
    }

    private void SpawnEnemy()
    {

    }
}
