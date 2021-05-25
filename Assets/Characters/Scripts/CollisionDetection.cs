using System;
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

        public Vector3 respawnLocation;
        
        private Vector2 previousVelocity;

        private void Start()
        {
            respawnLocation = gameObject.GetComponentInParent<Transform>().position;
            //throw new NotImplementedException();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            
            switch (collision.gameObject.layer)
            {
                case 12: //Death Pit
                    enemyPhysics.velocity = Vector2.zero;
                    break;
                case 14: //Room Trigger
                    return;
                //teleporter
                case 20: 
                {
                    GameObject teleporter; //more efficient this way
                    var teleporterScript = (teleporter = collision.gameObject).GetComponent<Teleporter>();
                
                    MainRigidbody2D.position = teleporterScript.teleportDestination.position;
                    var cooldown = TeleporterCooldown(teleporterScript.cooldownLength, teleporter, teleporterScript.secondTeleporter);
                    StartCoroutine(cooldown); //Makes it so that the enemy wont fall on to one of the teleporters and get stuck in an infinite loop
                
                    return;
                }
                default:
                    enemyPhysics.EnterBulletTime();

                    //Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            switch (other.gameObject.layer)
            {
                case 12:
                case 14:
                case 20:
                    return;
                default:
                    enemyPhysics.ExitBulletTime();
                    break;
            }
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
