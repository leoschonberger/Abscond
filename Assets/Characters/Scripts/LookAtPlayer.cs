using UnityEngine;

namespace Characters.Scripts
{
    public class LookAtPlayer : MonoBehaviour
    {
        public Transform target;
        public float speed = 3f;
  
        private void Update()
        {
            if (Vector3.Distance(transform.position, target.position) > 1f)
            {
                RotateTowards(target.position);
            }
        }
        
        private void RotateTowards(Vector2 target)
        {
            //var offset = 90f;
            Vector2 direction = target - (Vector2)transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle /*+ offset*/));
        }
    }
}
