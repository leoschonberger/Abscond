using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Characters.Scripts.PhysicsCode;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Characters.Scripts
{
    public class Health : MonoBehaviour
    {
        public int maxHp;
        public int currentHp;
        public float deathTimer;
        public int DeathCountdown;
        
        public bool isDead = false;
        
        public Vector2 updatedRespawnPoint;
        public GameObject player;
        public TextMesh tmesh;
        public GameObject DeathImage;

        public PlayerController playerController;

        // Start is called before the first frame update
        //Sets Ui box value and starting HP
        void Start()
        {
            currentHp = maxHp;
            UpdateHpCounter();
        }

        // Update is called once per frame
        void Update()
        {
            CheckDeath();
        } 
        //Allows gameObject to take damage, subtracted from currentHp
       public void TakeDamage(int damageToTake)
        {
            currentHp -= damageToTake;
            UpdateHpCounter();
        }
       
       //Healing: Adds hpToHeal to currentHp
       public void Heal(int hpToHeal)
        {
            currentHp += hpToHeal;
            UpdateHpCounter();
        }
       private void CheckDeath()
        {
            if (currentHp < 1)
            {
                isDead = true;
                //Debug.Log(gameObject.name + "Has died");
                if (player.tag == "Player")
                {
                    //TODO: Make PLayer Death Animation
                   //TODO: Implement Respawn Menu
                   Respawn();  
                }
                else if (player.tag == "Enemy")
                {
                     
                    //TODO: Make Enemy Death Animation
                    var deathAnimation = EnemyDeathAnimationWindow();
                    StartCoroutine(deathAnimation);
                    Destroy(player);
                }
            }
        }
       
       //Updates UI Text box with string
       private void UpdateHpCounter()
        {
            String hpText =" HP: " + currentHp;
            tmesh.text = hpText;
        }

       //Heals Player to maxHp, sets location to last respawn point, and resets isDead.
       private void Respawn()
       {
           Debug.Log("Respawning Player");
           Heal(maxHp);

           var death = DeathLength();
           StartCoroutine(death);

       }

       private IEnumerator EnemyDeathAnimationWindow()
       {
           yield return new WaitForSeconds(2);
       }
       private IEnumerator DeathLength()
       {
           playerController.enabled = false;
           DeathImage.SetActive(true);
           yield return new WaitForSeconds(deathTimer);
           DeathImage.SetActive(false);
           playerController.enabled = true;
           playerController.velocity = Vector2.zero;
           player.transform.position = updatedRespawnPoint;
           isDead = false;
       }

       private void OnTriggerEnter2D(Collider2D other)
       {
           if (other.gameObject.layer == 12) //If we land in the death pit
           {
               Respawn();
           }
           //throw new NotImplementedException();
       }
    }
}
