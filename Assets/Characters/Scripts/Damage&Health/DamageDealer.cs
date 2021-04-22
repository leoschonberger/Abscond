using System;
using UnityEngine;

namespace Characters.Scripts
{
    public class DamageDealer : MonoBehaviour
    {
        public int damage = 10;
        public BoxCollider2D attackCollider;

        // Checks to see if object is an enemy or player, then sends damage.
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject != null)
            {
                if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
                {
                    other.GetComponent<Health>().TakeDamage(damage);
                    //Debug.Log("Damage given to: " + other.gameObject.name + "From :" + this.gameObject.name + " HP is now: " + other.GetComponent<Health>().currentHp);
                }
            }
        }
    }
}
