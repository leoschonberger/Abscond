using Characters.Scripts;
using UnityEngine;

public class DeathPitRespawn : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hello");
            var collisionDetection = other.GetComponent<CollisionDetection>();
            collisionDetection.MainTransform.position = collisionDetection.respawnLocation;
        }
        
        //other.gameObject.transform.position = other.gameObject.GetComponent<PlayerController>().respawnLocation;
        //other.gameObject.transform.position = other.gameObject.GetComponentInChildren<PlayerController>().respawnLocation;
        //throw new NotImplementedException();
    }
}