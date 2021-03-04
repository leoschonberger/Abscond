using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //rotation
        Vector3 mousePos = Input.mousePosition; // Sets a vector variable "mosePos" = to the mouse position.
        mousePos.z = 5.23f; //Distance between camera and object/scene.
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);//Finds object position
        mousePos.x = mousePos.x - objectPos.x;//Point left/right comparing to objectPos
        mousePos.y = mousePos.y - objectPos.y;//Point up/down
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;//Calculate angle of pointing between 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));//Sets rotation based on angle
    }
}
