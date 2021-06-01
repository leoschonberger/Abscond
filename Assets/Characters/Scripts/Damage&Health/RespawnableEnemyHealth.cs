using System.Collections;
using UnityEngine;

namespace Characters.Scripts
{
    public class RespawnableEnemyHealth : EnemyHealth
    {
        public int deathLength = 1;
        protected override void Die()
        {
            StartCoroutine(respawnTimer(deathLength));
        }

        private IEnumerator respawnTimer(int deathLength)
        {
            yield return new WaitForSeconds(deathLength);
            base.Die();
        }
    }
    
}