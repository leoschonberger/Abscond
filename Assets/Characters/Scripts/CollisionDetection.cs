using System.Collections;
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
                
                GameObject teleporter; //more efficient this way
                var teleporterScript = (teleporter = collision.gameObject).GetComponent<Teleporter>();
                
                MainRigidbody2D.position = teleporterScript.teleportDestination.position;
                var cooldown = TeleporterCooldown(teleporterScript.cooldownLength, teleporter, teleporterScript.secondTeleporter);
                StartCoroutine(cooldown); //Makes it so that the enemy wont fall on to one of the teleporters and get stuck in an infinite loop
                
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

        private IEnumerator TeleporterCooldown(float cooldownTime, GameObject teleporter1, GameObject teleporter2)
        {
            teleporter1.SetActive(false);
            teleporter2.SetActive(false);
            yield return new WaitForSeconds(cooldownTime);
            teleporter1.SetActive(true);
            teleporter2.SetActive(true);
        }
    }
}
