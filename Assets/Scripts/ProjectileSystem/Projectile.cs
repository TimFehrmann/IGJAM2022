using System;
using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;

namespace ProjectileSystem
{
    public class Projectile : MonoBehaviour
    {
        [Header("Local References")] 
        [SerializeField]
        private SpriteRenderer projectileRenderer;
        [SerializeField] 
        private ParticleSystem explosionParticle;

        public Action<Projectile> OnDestruction;
        
        private ProjectileSettings projectileSettings;
        private Vector3 velocity;
        private Rigidbody2D rigid2d;

        private void Awake()
        {
            rigid2d = transform.GetComponent<Rigidbody2D>();
        }

        public void Init(ProjectileSettings settings)
        {
            projectileSettings = settings;
            projectileRenderer.color = projectileSettings.Color;
            transform.localScale =new Vector3(projectileSettings.Size, projectileSettings.Size, projectileSettings.Size);

            velocity = transform.up;
        }

        public void UpdateMovement()
        {
            transform.LookAtDirection2D(velocity);
            rigid2d.velocity = velocity * (projectileSettings.Speed * 100 * Time.deltaTime);
        }

        public void Destroy()
        {
            //explosionParticle.Play();

            if (OnDestruction != null)
            {
                OnDestruction(this);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (projectileSettings.ReflectionLayer.Select(x=> x.ToLayer()).Contains(other.gameObject.layer))
            {
                foreach (var contact in other.contacts)
                {
                    var qua = Quaternion.AngleAxis(180, contact.normal);

                    velocity = Quaternion.AngleAxis(180, contact.normal) * transform.up * -1;
                }
            }
            else
            {
                //TODO DMG Hinzufuegen;
                Destroy();
            }
        }
    }
}