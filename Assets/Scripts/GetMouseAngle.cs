using UnityEngine;

public static class GetMouseAngle
{
        public static float MouseAngle(Transform transform)
        {
                var mousePos = Input.mousePosition; // Sets a vector variable "mosePos" = to the mouse position.
                mousePos.z = 5.23f; //Distance between camera and object/scene.
                var objectPos = Camera.main.WorldToScreenPoint (transform.position);//Finds object position
                mousePos.x -= objectPos.x;//Point left/right comparing to objectPos
                mousePos.y -= objectPos.y;//Point up/down
                var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
                return angle;
        }
}