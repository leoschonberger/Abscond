using Characters.Scripts;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    public Transform respawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<HurtBoxInteractions>().SetTransform(respawnLocation);
        //Debug.Log("yay!");
        //throw new NotImplementedException();
    }
}
