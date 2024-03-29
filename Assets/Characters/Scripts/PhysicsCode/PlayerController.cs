using System;
using Unity.Mathematics;
using UnityEngine;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Characters.Scripts.PhysicsCode
{
    public class PlayerController : BasicPhysics
    {
        //public GameObject arm;
        //public GameObject attackLengthObject;
        //public TextMesh timeLeftInAttackText;
        
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;
        
        [Range(0.1f,0.5f)] public float timeOfDash = 0.1f; 
        [Range(14,56)] public float speedOfDash = 14f;
        [Range(14, 56)] public float attackStrength = 14f;

        public ComboSystem comboSystem;
        
        private float _timeLeftInDash;
        private Vector2 _dashDirection = Vector2.zero;
        private bool _canWeDashAgain = true;

        //private bool exitedBulletTime = false;
        private Animator PlayerAnimations;
        
        private bool exitedBulletTime = false;
        protected override void ComputeVelocity()
        {
            if (inBulletTime)
            {
                return;
            }

            /*if (exitedBulletTime)
            {
                exitedBulletTime = false;
                return;
            }*/
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
                    velocity.x = 0f;
                }
                return;
            }

            TargetVelocity = move * maxSpeed;
           // Debug.Log("move "+ move);
        
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
            Time.timeScale = 0.02f;
            Time.fixedDeltaTime *= Time.timeScale;
            //arm.SetActive(true);
            //attackLengthObject.SetActive(true);
        }

        protected override void OnLanding()
        {
            comboSystem.resetComboCounter();
        }
        public override void ExitBulletTime()
        {
            base.ExitBulletTime();
            //arm.SetActive(false);
            //attackLengthObject.SetActive(false);
            _timeLeftInDash = 0; //Resets our dash!
            velocity = Vector2.zero;
            TargetVelocity = Vector2.zero; //we make sure to cancel our velocity here

            var angle = GetMouseAngle.MouseAngle(transform) + 180;
            //Debug.Log(angle);

            var attackVelocity = new Vector2((float) (Math.Cos(angle * Mathf.Deg2Rad)) * attackStrength,
                (float) (Math.Sin(angle * Mathf.Deg2Rad)) * attackStrength);
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;

            velocity = attackVelocity;
            
            if (!IsGrounded)
                comboSystem.incrementComboCounter(); //Starts the combo!
            
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                Input.ResetInputAxes(); //We need this because unity is dumb and tries to pretend that wasd is analog instead of digital and messes everything up.
        }

        protected override void SetHorizontalVelocity() 
        
        {
            
            /*if (!IsGrounded)
            {
                Debug.Log(velocity);
                Debug.Log(TargetVelocity);
            }*/

            if (!IsGrounded && TargetVelocity == Vector2.zero)//shouldn't need this, but apparently I do
            {
                return;
            }
            if (IsGrounded && math.abs(velocity.x)<=maxSpeed || !IsGravityEnabled)
            {
                
                base.SetHorizontalVelocity();
                return;
            }

            if (IsGrounded&& TargetVelocity.x==0f)
            {
                if (velocity.x<0)
                {
                    velocity.x += maxSpeed/4;
                }

                if (velocity.x>0)//program in friction on the ground eventuaally
                {
                    velocity.x -= maxSpeed / 4;
                    
                    
                }
                
                //add check if we went past zero
            }

            //This could use some cleaning up.
            if (math.abs(velocity.x)<=maxSpeed)
            {
                velocity.x += TargetVelocity.x / 2;
                if (math.abs(velocity.x)>maxSpeed) // checks if we went past zero
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
                    velocity.x += TargetVelocity.x/4;
                //Debug.Log("whyy, target velocity: " +TargetVelocity);
            }
            else if (velocity.x>0 && TargetVelocity.x<0)
            {
                if (velocity.x>maxSpeed)
                    velocity.x += TargetVelocity.x / 4;
                //Debug.Log("whyy, target velocity: " +TargetVelocity);
            }

        }
    }
}
