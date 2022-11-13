using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class BlockBehaviour : MonoBehaviour, IPlaceableBehaviour
    {
        [Header("Local references")]
        [SerializeField]
        private Collider2D itemCollider;

        public void Despawn()
        {
            itemCollider.enabled = false;
        }

        public bool IsPlaced()
        {
            return true;
        }

        public void OnPlacementUpdate()
        {
            //empty
        }

        public void Place()
        {
            itemCollider.enabled = true;
        }
    }
}