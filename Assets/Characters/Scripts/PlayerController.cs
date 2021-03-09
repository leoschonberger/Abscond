using System;
using Unity.Mathematics;
using UnityEngine;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Characters.Scripts
{
    public class PlayerController : BasicPhysics
    {
        public GameObject arm;
        
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;
        
        [Range(0.1f,0.5f)] public float timeOfDash = 0.1f; 
        [Range(14,56)] public float speedOfDash = 14f;
        [Range(14, 56)] public float attackStrength = 14f;
        
        private float _timeLeftInDash;
        private Vector2 _dashDirection = Vector2.zero;
        private bool _canWeDashAgain = true;

        private bool exitedBulletTime = false;
        protected override void ComputeVelocity()
        {
            if (inBulletTime)
                return;
            if (exitedBulletTime)
            {
                exitedBulletTime = false;
                return;
            }
            var move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
            if (IsGrounded&& _timeLeftInDash ==0f && !Input.GetKey(KeyCode.LeftShift)) //Allows us to dash again if we are on the ground and not holding the button
            {
                _canWeDashAgain = true;
            }
            if (Input.GetKey(KeyCode.LeftShift) && _timeLeftInDash == 0f && _canWeDashAgain) //Checks if we can dash again
            {
                //Sets our dashDirection vector to the correct values
                if (move.x != Vector2.zero.x) //Later add a buffer window
                    _dashDirection.x = Math.Abs(move.x) * (speedOfDash / move.x); 
                if (move.y != Vector2.zero.y)
                    _dashDirection.y = Math.Abs(move.y) * (speedOfDash / move.y);

                _canWeDashAgain = false;
                
                _timeLeftInDash = timeOfDash; //Sets the countdown timer for the dash
                
            }

            if (IsGrounded && Input.GetButtonDown("Jump")) //Checks if we can jump, doesn't matter if in dash, velocity gets rewritten later on in the file.
                velocity.y = jumpTakeOffSpeed;
            else if (Input.GetButtonUp("Jump"))// makes it so jump height dependent on how long we hold the button
            {
                if (velocity.y>0)
                    velocity.y *= .5f;
            }
            if (_timeLeftInDash != 0f) //If we are in a dash, do the dash things
            {
                _timeLeftInDash -= Time.deltaTime;
                DashMove();
                if (_timeLeftInDash <= 0f) //If dash is over, reset everything se we don't go flying 
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
            //Debug.Log(_dashDirection);
            velocity.y = _dashDirection.y;
            
        }

        public override void EnterBulletTime()
        {
            base.EnterBulletTime();
            arm.SetActive(true);
        }

        public override void ExitBulletTime()
        {
            base.ExitBulletTime();
            arm.SetActive(false);
            _timeLeftInDash = 0;
            velocity = Vector2.zero;
            TargetVelocity = Vector2.zero; //we make sure to cancel our velocity here
            
            var angle= GetMouseAngle.MouseAngle(transform) +180;
            
            var attackVelocity = new Vector2((float)Math.Cos(angle)*attackStrength, //*Mathf.Rad2Deg
            (float)Math.Sin(angle)*attackStrength);
            
            Debug.Log(angle);
            //Debug.Log(attackVelocity);
            //attackVelocity = Quaternion.AngleAxis(angle , Vector3.up) * attackVelocity;
            Debug.Log(attackVelocity);
            
            velocity = attackVelocity;
            //exitedBulletTime = true;
        }

        protected override void SetHorizontalVelocity()
        {
            if (IsGrounded && math.abs(velocity.x)<=maxSpeed)
            {
                base.SetHorizontalVelocity();
                return;
            }
            
            
            //This could use some cleaning up.
            if (math.abs(velocity.x)<=maxSpeed)
            {
                velocity.x += TargetVelocity.x / 2;
                if (math.abs(velocity.x)>maxSpeed)
                {
                    if (velocity.x < 0)
                        velocity.x = -maxSpeed;

                    else
                        velocity.x = maxSpeed;

                }
                return;
            }
            if (velocity.x<0 && TargetVelocity.x>0) //If we are headed backwards and but want to move forwards
            {
                if (velocity.x<-maxSpeed) //If our velocity is greater in magnitude than the max speed allowed
                    velocity.x += TargetVelocity.x/2;
            }
            else if (velocity.x>0 && TargetVelocity.x<0)
            {
                if (velocity.x>maxSpeed)
                    velocity.x += TargetVelocity.x / 2;
            }
            
            
        }
    }
}
