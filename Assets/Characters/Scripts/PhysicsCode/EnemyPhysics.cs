using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Characters.Scripts.PhysicsCode
{
    public class EnemyPhysics : BasicPhysics
    {
        public float speedOfLaunch = 28f;

        public float frictionValue = 7f;
        public float bounceLength = 0.3f;
        public float attackStrength = 28f;

        public Transform playerTransform;
        private float timeLeftInBounce = 0f;
        [SerializeField] private float currentAngle = 0f;

        // ReSharper disable CompareOfFloatsByEqualityOperator

        protected override void ComputeVelocity()
        {
            enemyMovement();
            if (timeLeftInBounce > 0)
            {
                //Debug.Log(Time.deltaTime);
                timeLeftInBounce -= Time.deltaTime;
                if (timeLeftInBounce<= 0)
                {
                    timeLeftInBounce = 0;
                    IsGravityEnabled = true;
                    inBounceMode = false; //turns off bounce mode when gravity turns back on
                    velocity.x /= 2;
                    velocity.y /= 2;
                    currentAngle = 0; //resets the current angle
                    //Debug.Log("hellooo");
                }
            }
            GroundedCheck();

            
        }
		protected virtual void enemyMovement () {}
        

        public override void ExitBulletTime()
        {
            base.ExitBulletTime();
            velocity = Vector2.zero;
            TargetVelocity = Vector2.zero;
            currentAngle= GetMouseAngle.MouseAngle(playerTransform);
            //Debug.Log("mouse angle: "+currentAngle);
            
            var attackVelocity = new Vector2((float)Math.Cos(currentAngle*Mathf.Deg2Rad)*speedOfLaunch, //*Mathf.Rad2Deg
                (float)Math.Sin(currentAngle*Mathf.Deg2Rad)*speedOfLaunch);
            //Debug.Log();
            velocity = attackVelocity;
            timeLeftInBounce = bounceLength;
            inBounceMode = true; //Tells physics engine to pay attention to bounce mode.
            IsGravityEnabled = false;

        }
        
        protected virtual void GroundedCheck(){}
        protected override void bouncePhysics(Vector2 move)
        {
            //Steps needed:
                //You should probably end up moving this to its own file
                
                //Check for if the player is hitting a collider or not. Steal the code you already have
                //get angle of ground vector.
                    //This is simple trig, we have x and y value.
                    //Arctan works for this, opposite over adjacent. y/x.
                        //Be careful about dividing by zero
                    //That will spit out the angle in radians
                //Take current angle
                //Calculate new angle to bounce
                    //currentAngle = 2arcTan(groundAngle) - currentangle
                //set grounded to false
                //set the current velocity to the desired velocity.
                
                //What happens if we bounce the guy off the wall?
                    //Unity is super nice, and gives us the desired normal no matter what.
                    
            //Step 1: check for if you are hitting a collider
            
            //Debug.Log(move);
            //Debug.Log("fixedDeltaTime: " +Time.fixedDeltaTime);
            /*if (inBulletTime)
                return;*/
            
            //Debug.Log("velocity "+ velocity);
            var distance = move.magnitude;
            //Debug.Log(move);
            var count= rb2dThatWeCast.Cast(move, ContactFilter, HitBuffer, distance + ShellRadius); //Counts the colliders we will contact within the next frame
            HitBufferList.Clear();
            for (int i = 0; i < count; i++)
                HitBufferList.Add(HitBuffer[i]);
            for (int i = 0; i < count; i++)
            {
                //Debug.Log(HitBufferList[i].collider.gameObject.name);
                var currentNormal = HitBufferList[i].normal;
                var angleOfCollider = new Vector2(currentNormal.y, -currentNormal.x); //This is the correct vector now

                float colliderAngle;
                
                //Step 2: Get the angle from the vector
                if (angleOfCollider.x == 0)
                {
                    //Check for dividing by zero
                    colliderAngle = 90;
                    //Debug.Log("hey wtf");
                } //NOTE: this might be wrong, it could be negative 90
                else
                    colliderAngle = (float)Math.Atan(angleOfCollider.y / angleOfCollider.x)*Mathf.Rad2Deg; //This is the angle of the ground
                
                //Step 3: calculate new angle to bounce
                //Debug.Log("collider angle: " +colliderAngle + "current angle: " + currentAngle);
                currentAngle = 2 * colliderAngle - currentAngle;// this should be our bouncy angle 
                //Debug.Log(currentAngle);
                
                //Step 4: set up everything so that the player will move.
                
                var bounceVelocity = new Vector2((float)Math.Cos(currentAngle*Mathf.Deg2Rad)*speedOfLaunch, 
                    (float)Math.Sin(currentAngle*Mathf.Deg2Rad)*speedOfLaunch);
                velocity = bounceVelocity;
            }
            
            //Debug.Log(move);
            move = velocity* Time.deltaTime;
            //Debug.Log(move);
            //Step 5: Move the player
            rb2d.position += move; //Moves the player

        }


        protected override void SetHorizontalVelocity()
        {
            
            if (TargetVelocity.x == 0 && IsGrounded)
            {
                    //Debug.Log(velocity);
                if (velocity.x >0)
                {
                    velocity.x -= frictionValue / 8;
                    if (velocity.x <0)// if we went past zero, set to zero
                        velocity.x = 0;
                    
                }

                else if (velocity.x < 0)
                {
                    velocity.x += frictionValue / 8;
                    if (velocity.x > 0)
                        velocity.x = 0;
                }
                return;
            }

            /*if (TargetVelocity.x == 0 && !IsGrounded)
            {
                if (velocity.x >0)
                {
                    velocity.x -= frictionValue / 16;
                    if (velocity.x <0)// if we went past zero, set to zero
                        velocity.x = 0;
                    
                }

                else if (velocity.x < 0)
                {
                    velocity.x += frictionValue / 16;
                    if (velocity.x > 0)
                        velocity.x = 0;
                }
                return;
            }*/ // Air friction, but not important right now. If we did want it, it should only occur when velocity.y < 0

            if (TargetVelocity.x !=0)
                base.SetHorizontalVelocity();
            
            //base.SetHorizontalVelocity();
        }
        
        
        
    }
}
