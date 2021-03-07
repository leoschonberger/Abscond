using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAtMouse : MonoBehaviour
{
    //public Transform playerLocation;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //rotation
        var angle = GetMouseAngle.MouseAngle(transform);
        //Calculate angle of pointing between 
        //transform.RotateAround(playerLocation.position, Vector3.up, angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));//Sets rotation based on angle
    }
}
