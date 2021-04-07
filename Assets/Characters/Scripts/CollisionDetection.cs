using Characters.Scripts.PhysicsCode;
using UnityEngine;

namespace Characters.Scripts
{
    public class CollisionDetection : MonoBehaviour
    {
        public EnemyPhysics enemyPhysics;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 14)
            {
                return;
            }

            enemyPhysics.EnterBulletTime();

                //Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 14)
            {
                return;
            }
            enemyPhysics.ExitBulletTime();
            //Debug.Log("this actually works");
            //throw new NotImplementedException();
        }
        
        
    }
}
