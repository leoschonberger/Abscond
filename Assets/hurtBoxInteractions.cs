using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtBoxInteractions : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTransform(Transform transform)
    {
        playerTransform.position = transform.position;
    }
     
}
