using UnityEngine;

namespace Characters.Scripts
{
    public class HurtBoxInteractions : MonoBehaviour
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

        public void SetTransform(Transform location)
        {
            playerTransform.position = location.position;
        }
     
    }
}
