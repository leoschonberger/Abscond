using Characters.Scripts.PhysicsCode;
using UnityEngine;

namespace Characters.Scripts
{
    public class CollisionDetection : MonoBehaviour
    {
        public EnemyPhysics enemyPhysics;

        public Rigidbody2D MainRigidbody2D;
        public Transform MainTransform;
        
        private Vector2 previousVelocity;
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 14)
            {
                return;
            }
            if (collision.gameObject.layer == 20) //teleporter
            {
                //Debug.Log("huh");
                //var velocity = enemyPhysics.velocity;
                //Debug.Log("saved velocity "+velocity);
                //previousVelocity = enemyPhysics.velocity;
                //enemyPhysics.velocity = previousVelocity;
                //MainRigidbody2D.Sleep();
                MainRigidbody2D.position = collision.gameObject.GetComponent<Teleporter>().teleportDestination.position;
                

                //Debug.Log("new velocity" + velocity);
                //Debug.Log(enemyPhysics.velocity);
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

            if (other.gameObject.layer == 20)
            {
                //MainRigidbody2D.WakeUp();
                //enemyPhysics.velocity = previousVelocity;
                //Debug.Log(enemyPhysics.velocity);
                return;
            }
            
            enemyPhysics.ExitBulletTime();
            //Debug.Log("this actually works");
            //throw new NotImplementedException();
        }
        
        
    }
}
