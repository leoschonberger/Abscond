using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportDestination;
    public GameObject secondTeleporter; //seems inefficient to do this this way, but that efficiency not very important right now
    
    public float cooldownLength = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //other.gameObject.transform
        throw new NotImplementedException();
    }
    
    
}
