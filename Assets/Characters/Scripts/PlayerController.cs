using System;
using UnityEngine;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Characters.Scripts
{
    public class PlayerController : BasicPhysics
    {
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;
        
        [Range(0.1f,0.5f)]
        public float timeOfDash = 0.1f; 
        [Range(14,56)]
        public float speedOfDash = 14f; 

        private float _timeLeftInDash;
        private Vector2 _dashDirection = Vector2.zero;
        private bool _canWeDashAgain = true;
        
        // Start is called before the first frame update

        protected override void ComputeVelocity()
        {
        var move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        if (IsGrounded&& _timeLeftInDash ==0f )
        {
            _canWeDashAgain = true;
        }
        if (Input.GetKey(KeyCode.LeftShift) && _timeLeftInDash == 0f && _canWeDashAgain)
        {
            
            if (move.x != Vector2.zero.x) //Later add a buffer window
                _dashDirection.x = Math.Abs(move.x) * (speedOfDash / move.x);
            if (move.y != Vector2.zero.y)
                _dashDirection.y = Math.Abs(move.y) * (speedOfDash / move.y);

            _canWeDashAgain = false;
            
            _timeLeftInDash = timeOfDash;
            
        }

        if (IsGrounded && Input.GetButtonDown("Jump"))
            velocity.y = jumpTakeOffSpeed;
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y>0)
                velocity.y *= .5f;
        }
        if (_timeLeftInDash != 0f)
        {
            _timeLeftInDash -= Time.deltaTime;
            DashMove();
            if (_timeLeftInDash <= 0f)
            {
                _timeLeftInDash = 0f;
                _dashDirection = Vector2.zero;
                IsGravityEnabled = true;
                velocity.y = 0f;
            }
            return;
        }

        TargetVelocity = move * maxSpeed;
        
        }

        private void DashMove()
        {

            IsGravityEnabled = false; //turns gravity off
            TargetVelocity.x = _dashDirection.x;
            Debug.Log(_dashDirection);
            velocity.y = _dashDirection.y;
            
        }
        
        
    }
}
