using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileSystem
{
    public class ProjectileController : MonoBehaviour
    {
        [Header("Canon settings")] 
        [SerializeField]
        private bool isActive;
        
        [Header("Projectile settings")] 
        [SerializeField] 
        private float spawnInterval;
        [SerializeField] 
        private ProjectileSettings projectileSettings;

        [Header("Local references")] 
        [SerializeField]
        private Transform projectileSpawnPosition;
        [SerializeField]
        private Projectile projectilePrefab;
        [SerializeField] 
        private Transform tankBarrel;
        [SerializeField] 
        private List<Transform> targetPositions;

        private int currentTargetIndex;
        private Transform currentTargetTransform;
        private ObjectPool<Projectile> projectilePool;
        private List<Projectile> activeProjectiles;
        private Coroutine projectileCoroutine;

        private void Awake()
        {
            projectilePool = new ObjectPool<Projectile>(20, projectilePrefab);
            activeProjectiles = new List<Projectile>();
            currentTargetIndex = -1;
        }

        private void FixedUpdate()
        {
            OnUpdate();
        }

        public void OnUpdate()
        {
            if (isActive && projectileCoroutine == null)
            {
                SetNextTarget();
                projectileCoroutine = StartCoroutine(ProjectileSpawnCorutine());
            }
            else if (!isActive && projectileCoroutine != null)
            {
                StopCoroutine(projectileCoroutine);
                projectileCoroutine = null;
            }

            for (int i = 0; i < activeProjectiles.Count; i++)
            {
                activeProjectiles[i].UpdateMovement();
            }
        }

        private void SetNextTarget()
        {
            if (targetPositions.Count == 0)
            {
                currentTargetIndex = -1;
                return;
            }

            if (currentTargetIndex + 1 >= targetPositions.Count)
            {
                currentTargetIndex = 0;
            }
            else
            {
                currentTargetIndex++;
            }

            currentTargetTransform = targetPositions[currentTargetIndex];
            tankBarrel.LookAt2D(currentTargetTransform);
        }
        
        private IEnumerator ProjectileSpawnCorutine()
        {
            while (isActive)
            {
                SpawnProjectile();
                yield return new WaitForSeconds(spawnInterval / 2);
                SetNextTarget();
                yield return new WaitForSeconds(spawnInterval / 2);
            }
        }

        private void SpawnProjectile()
        {
            var projectile = projectilePool.GetObject();
            projectile.transform.SetPositionAndRotation(projectileSpawnPosition.position,
                projectileSpawnPosition.rotation);
            projectile.Init(projectileSettings);
            projectile.gameObject.SetActive(true);
            activeProjectiles.Add(projectile);

            projectile.OnDestruction += OnProjectileDestruction;
        }

        private void OnProjectileDestruction(Projectile projectile)
        {
            activeProjectiles.Remove(projectile);
            projectilePool.ReturnToPool(projectile);

            projectile.OnDestruction -= OnProjectileDestruction;
        }
    }
}