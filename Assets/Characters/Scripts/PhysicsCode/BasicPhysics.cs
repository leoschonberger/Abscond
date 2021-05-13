using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Scripts.PhysicsCode
{
    public class BasicPhysics : MonoBehaviour
    {
        public float minGroundNormalY = 0.65f;
        public float gravityModifier = 1f;

        
        public bool IsGrounded;
        protected Vector2 GroundNormal = Vector2.up;

        protected Vector2 TargetVelocity;
        [FormerlySerializedAs("Velocity")] public Vector2 velocity;
        [FormerlySerializedAs("Rb2d")] public Rigidbody2D rb2d; //This RigidBody2D is what allows us to move the character and the sprite around. 
        [FormerlySerializedAs("Rb2dThatWeCast")] public Rigidbody2D rb2dThatWeCast; //This RigidBody checks for collisions, but only with the one collider we want it to check with.
        protected ContactFilter2D ContactFilter;
        protected RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
        protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);

        protected const float MinMoveDistance = 0.001f;
        protected const float ShellRadius = 0.01f;

        protected bool inBulletTime = false;
        protected bool inBounceMode = false;

        protected bool IsGravityEnabled = true;
        // Start is called before the first frame update


        void Start()
        {
            ContactFilter.useTriggers= false;
            ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            //Debug.Log(Time.timeScale);
            ContactFilter.useLayerMask = true;
        }

        protected virtual void ComputeVelocity(){} //Overriden in player controller
        // Update is called once per frame
        void Update()
        {
            TargetVelocity = Vector2.zero;
            //Debug.Log("hey");
            ComputeVelocity();
            
        }
    
        void FixedUpdate()
        {
            if (inBulletTime)
            {
                IsGrounded = false;
                return;
            }
            if (inBounceMode) //do bounce physics when you are in bounce mode
            {
                //Debug.Log("bounce mode "+ velocity);
                //var previousVelocity = velocity;
                bouncePhysics(velocity * Time.fixedDeltaTime);
                
                return;
            }

            if (IsGravityEnabled)
            {
                //Debug.Log("current velocity "+ velocity);
                var preivousVelocity = velocity;
                velocity += Physics2D.gravity * (gravityModifier * Time.fixedDeltaTime); //serves as friction as well
                if (preivousVelocity.y > 0 && velocity.y <0 )
                {
                    Debug.Log("what");
                }
            }

            
            
            SetHorizontalVelocity();

            //Debug.Log(velocity);

            IsGrounded = false;//assumes you are not grounded at the beginning

            var deltaPosition = velocity * Time.fixedDeltaTime; //this is the change in position on the next update
            
            var moveAlongGround = new Vector2(GroundNormal.y, -GroundNormal.x); //reversing the normal to tell character what direction they should move up slope

            var move = moveAlongGround * deltaPosition.x; //this is how far we want to move along x

            Movement(move, false); //moves along x
        
            move = Vector2.up * deltaPosition.y;
        
            Movement(move, true); //moves along y
        
        }
        
        protected virtual void bouncePhysics(Vector2 move){}
        
        void Movement(Vector2 move, bool yMovement)
        {
            var distance = move.magnitude; //Saves the speed, which is going to be changed when we modify the distance

            if (distance > MinMoveDistance) //If gameobject is moving perform extra check
            {
                distance = ModifyDistance(move, distance, yMovement); //collision detection
            }

            
            //Debug.Log();
            
            rb2d.position += move.normalized * (distance * Time.timeScale); //actually moves the rigidbody
        }

        protected virtual void SetHorizontalVelocity()
        {
            
            velocity.x = TargetVelocity.x;
            

        }

        private float ModifyDistance(Vector2 move, float distance, bool yMovement)
        {
        
            var count= rb2dThatWeCast.Cast(move, ContactFilter, HitBuffer, distance + ShellRadius); //Counts the colliders we will contact within the next frame
            HitBufferList.Clear();
            for (int i = 0; i < count; i++)
                HitBufferList.Add(HitBuffer[i]);
       
            for (int i = 0; i < count; i++)
            {
                //Debug.Log(hitBufferList[i].collider.name);
                var currentNormal = HitBufferList[i].normal;
                //GroundNormal = currentNormal;
                if (currentNormal.y > minGroundNormalY) //checks if you are on the ground or not
                {
                    IsGrounded = true;
                
                    if (yMovement) //if moving on y axis and grounded, we set the current normal to be straight up and down (don't want vertical movement to be affected by slopes)
                    {
                        GroundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                

                var projection =
                    Vector2.Dot(velocity,
                        currentNormal); //Projects  to find whether we are about to clip through the ground
                if (projection < 0 ) //If we will clip into ground next frame, change velocity so that won't happen.
                {
                    //Debug.Log(projection);
                    velocity -= projection * currentNormal;
                    //Debug.Log(velocity.x);
                }

                

                var modifiedDistance = HitBufferList[i].distance - ShellRadius;
                
                distance = modifiedDistance < distance
                    ? modifiedDistance
                    : distance; //If distance is greater than modified distance, we change it to become modified distance, preventing clipping
            }

            return distance;
        } // this is the most complicated part, I don't want to completely just copy what I had right now
        public virtual void EnterBulletTime()
        {
            inBulletTime = true;
        }

        public virtual void ExitBulletTime()
        {
            inBulletTime = false;
        }
    }
    
}
