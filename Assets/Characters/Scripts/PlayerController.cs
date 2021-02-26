using UnityEngine;

namespace Characters.Scripts
{
    public class PlayerController : BasicPhysics
    {
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;
        // Start is called before the first frame update
        void Start()
        {
            ContactFilter.useTriggers= false;
            ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        
            ContactFilter.useLayerMask = true;
        }

        protected override void OnStartUp()
        {
        
        }
        

        protected override void ComputeVelocity()
        {
        var move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        Debug.Log("hey");

        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            Velocity.y = jumpTakeOffSpeed;
            
        }

        TargetVelocity = move * maxSpeed;
        }
    }
}
