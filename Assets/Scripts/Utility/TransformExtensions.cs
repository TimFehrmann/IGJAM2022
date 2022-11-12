using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class TransformExtensions
    {
        public static void LookAt2D(this Transform transform, Transform target)
        {
            Vector3 diff = target.position - transform.position;
            diff.Normalize();
 
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        
        public static void LookAtDirection2D(this Transform transform, Vector3 direction)
        {
            Vector3 diff = direction.normalized;
            diff.Normalize();
 
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }
}