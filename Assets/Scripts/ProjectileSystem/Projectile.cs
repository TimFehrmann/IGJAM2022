using System;
using System.Linq;
using Assets.Scripts.Utility;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Local References")]
    [SerializeField]
    private SpriteRenderer projectileRenderer;
    [SerializeField]
    private ParticleSystem explosionParticle;

    public Action<Projectile> OnDestruction;

    private ProjectileSettings projectileSettings;
    private Vector3 projectileVelocity;
    private Rigidbody2D rigid2d;

    public Vector3 ProjectileVelocity { get => projectileVelocity; private set => projectileVelocity = value; }

    private void Awake()
    {
        rigid2d = transform.GetComponent<Rigidbody2D>();
    }

    public void Init(ProjectileSettings settings)
    {
        projectileSettings = settings;
        projectileRenderer.color = projectileSettings.Color;
        transform.localScale = new Vector3(projectileSettings.Size, projectileSettings.Size, projectileSettings.Size);

        ProjectileVelocity = transform.up;
    }

    public void UpdateMovement()
    {
        transform.LookAtDirection2D(ProjectileVelocity);
        rigid2d.velocity = ProjectileVelocity * (projectileSettings.Speed * 100 * Time.deltaTime);
    }

    public void Destroy()
    {
        //explosionParticle.Play();

        if (OnDestruction != null)
        {
            OnDestruction(this);
        }
    }

    public void CopyValuesFromOtherProjectile(Projectile otherProjectile)
    {
        projectileVelocity = otherProjectile.ProjectileVelocity;
        transform.position = otherProjectile.transform.position;
        transform.rotation = otherProjectile.transform.rotation;
        transform.localScale = otherProjectile.transform.localScale;
    }

    public void Redirect(int zAngle)
    {
        projectileVelocity = Quaternion.Euler(0, 0, zAngle) * projectileVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (projectileSettings.ReflectionLayer.Select(x => x.ToLayer()).Contains(other.gameObject.layer))
        {
            foreach (var contact in other.contacts)
            {
                ProjectileVelocity = Quaternion.AngleAxis(180, contact.normal) * transform.up * -1;
            }
        }
        else
        {
            var enemy= other.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Destroy();
            }
            
            
            //TODO DMG Hinzufuegen;
            Destroy();
        }
    }
}
