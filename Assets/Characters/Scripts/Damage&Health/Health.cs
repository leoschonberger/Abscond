using System;
using UnityEngine;
using UnityEngine.UI;
namespace Characters.Scripts
{
    public class Health : MonoBehaviour
    {
        public int maxHp;
        public int currentHp;
        public bool isDead = false;
        public Text hpUiBox;


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
        //Allows gameObject to take damage
       public void TakeDamage(int damageToTake)
        {
            currentHp -= damageToTake;
            UpdatehpUiBox();
        }
       
       //Healing: included just in case we want to add it in later
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
                //Play Death Animation
                // Open Respawn Menu
            }
        }
       
       //Updates UI Text box with string
       private void UpdatehpUiBox()
        {
            String hpText = gameObject.name + " HP: " + currentHp;
            hpUiBox.text = hpText;
        }
    }
}
