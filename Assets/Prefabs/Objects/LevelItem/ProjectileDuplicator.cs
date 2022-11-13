using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileDuplicator : MonoBehaviour, IPlaceableBehaviour
{
    [SerializeField] private List<int> duplicationAngles = new List<int>();

    // Cached
    private Collider2D collider2D;
    private ProjectileController projectileController;
    private List<Projectile> duplicateProjectilesToIgnoreOnce;


    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        projectileController = FindObjectOfType<ProjectileController>();

        collider2D.enabled = false;
    }

    private void DuplicateAndRedirectProjectiles(Projectile otherProjectile)
    {
        foreach(int angle in duplicationAngles)
        {
            DuplicateAndRedirectProjectile(otherProjectile, angle);
        }

    }

    private void DuplicateAndRedirectProjectile(Projectile otherProjectile, int angle)
    {
        // Create new Projectile
        Projectile newProjectile = projectileController.SpawnProjectile();

        // Copy Projectile Settings from existing one
        newProjectile.CopyValuesFromOtherProjectile(otherProjectile);

        // Redirect
        //Vector3 eulerAngles = newProjectile.transform.eulerAngles;
        //eulerAngles.z += angle;
        //newProjectile.transform.eulerAngles = eulerAngles;
        newProjectile.Redirect(angle);

        // Ignore OnTriggerEnter2D once after spawn
        duplicateProjectilesToIgnoreOnce.Add(newProjectile);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();

        if (!projectile)
        {
            return;
        }

        // Avoid Infinite duplication of duplicates spawned inside Trigger Zone
        bool wasSpawnedByThis = duplicateProjectilesToIgnoreOnce.Contains(projectile);
        if (wasSpawnedByThis)
        {
            duplicateProjectilesToIgnoreOnce.Remove(projectile);
            return;
        }

        DuplicateAndRedirectProjectiles(projectile);
    }

    public void Place()
    {
        collider2D.enabled = true;
        duplicateProjectilesToIgnoreOnce = new List<Projectile>();
    }

    public void Despawn()
    {
        collider2D.enabled = false;
    }

    public void OnPlacementUpdate()
    {
        
    }

    public bool IsPlaced()
    {
        return collider2D.enabled == true;
    }
}
