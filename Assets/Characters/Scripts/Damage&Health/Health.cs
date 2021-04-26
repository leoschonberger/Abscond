using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Characters.Scripts
{
    public class Health : MonoBehaviour
    {
        public int maxHp;
        public int currentHp;
        public bool isDead = false;
        public Text hpUiBox;
        public Vector2 updatedRespawnPoint;
        public GameObject player;


        // Start is called before the first frame update
        //Sets Ui box value and starting HP
        void Start()
        {
            currentHp = maxHp;
            UpdatehpUiBox();
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
            //UpdatehpUiBox();
        }
       
       //Healing: Adds hpToHeal to currentHp
       public void Heal(int hpToHeal)
        {
            currentHp += hpToHeal;
            UpdatehpUiBox();
        }
       private void CheckDeath()
        {
            if (currentHp < 1)
            {
                isDead = true;
                Debug.Log(gameObject.name + "Has died");
                if (player.tag == "Player")
                {
                    //TODO: Make PLayer Death Animation
                   //TODO: Implement Respawn Menu
                   Respawn();  
                }
                else if (player.tag == "Enemy")
                {
                    //TODO: Make Enemy Death Animation
                    Destroy(player);
                }
            }
        }
       
       //Updates UI Text box with string
       private void UpdatehpUiBox()
        {
            String hpText = gameObject.name + " HP: " + currentHp;
            hpUiBox.text = hpText;
        }

       //Heals Player to maxHp, sets location to last respawn point, and resets isDead.
       private void Respawn()
       {
           Debug.Log("Respawning Player");
           Heal(maxHp);
           Debug.Log("Player has regained health");
           player.transform.position = updatedRespawnPoint;
           isDead = false;

       }
    }
}
