using UnityEngine;
using UnityEngine.UIElements;

namespace Characters.Scripts
{
    public class PlayerController : BasicPhysics
    {
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public float timeOfDash = 0.5f;
        public float speedOfDash = 14f;

        private float timeLeftInDash = 0f;
        private Vector2 dashDirection = Vector2.zero;
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
        
        if (Input.GetKey(KeyCode.LeftShift) && timeLeftInDash == 0f)
        {
            dashDirection.x = speedOfDash * move.x;
            timeLeftInDash = timeOfDash;
        }

        

        if (timeLeftInDash != 0f)
        {
            
            DashMove(move);
            if (timeLeftInDash <= 0f)
            {
                timeLeftInDash = 0f;
                isGravityEnabled = true;
            }
        }
        

        if (IsGrounded && Input.GetButtonDown("Jump"))
            Velocity.y = jumpTakeOffSpeed;
        else if (Input.GetButtonUp("Jump"))
        {
            if (Velocity.y>0)
                Velocity.y *= .5f;
        }

        TargetVelocity = move * maxSpeed;
        }

        private void DashMove(Vector2 move)
        {
            isGravityEnabled = false;
            TargetVelocity.x = dashDirection.x;
            Velocity.y = 0f;
            timeLeftInDash -= Time.deltaTime;
        }
    }
}
