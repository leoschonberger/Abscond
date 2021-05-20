using Characters.Scripts;
using UnityEngine;

public class DeathPitRespawn : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("hello");
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<Transform>().position = other.gameObject.GetComponent<CollisionDetection>().respawnLocation;
        }
        
        //other.gameObject.transform.position = other.gameObject.GetComponent<PlayerController>().respawnLocation;
        //other.gameObject.transform.position = other.gameObject.GetComponentInChildren<PlayerController>().respawnLocation;
        //throw new NotImplementedException();
    }
}