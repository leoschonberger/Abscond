using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPhysics : MonoBehaviour
{
    public float minGroundNormalY = 0.65f;
    public float gravityModifier = 1f;

    protected bool IsGrounded;
    protected Vector2 GroundNormal;

    protected Vector2 TargetVelocity;
    public Vector2 Velocity;
    public Rigidbody2D Rb2d;
    protected ContactFilter2D ContactFilter;

    protected const float MinMoveDistance = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        ContactFilter.useTriggers= false;
        LayerMask layerMask = LayerMask.GetMask("Player");
        ContactFilter.SetLayerMask(layerMask);
            
        ContactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        TargetVelocity = Vector2.zero;
        ComputeVelocity();
    }
    
    protected virtual void ComputeVelocity(){} //Overridden in player controller

    void FixedUpdate()
    {
        Velocity += Physics2D.gravity * (gravityModifier * Time.deltaTime);
        Velocity.x = TargetVelocity.x;
        
        IsGrounded = false;

        var deltaPosition = Velocity * Time.deltaTime; //this is the change in position on the next update
            
        var moveAlongGround = new Vector2(GroundNormal.y, -GroundNormal.x); //reversing the normal to tell character what direction they should move up slope

        var move = moveAlongGround * deltaPosition.x; //this is how far we want to move along x
        
        Movement(move, false); //moves along x
        
        move = Vector2.up * deltaPosition.y;
        
        Movement(move, true); //moves along y
        throw new NotImplementedException();
    }

    void Movement(Vector2 move, bool yMovement)
    {
        var distance = move.magnitude;

        if (distance > MinMoveDistance) //If gameobject is moving perform extra check
        {
            distance = ModifyDistance(move, distance, yMovement); //collision detection
        }
            
        Rb2d.position += move.normalized*distance; //actually moves the rigidbody
    }

    private float ModifyDistance(Vector2 move, float distance, bool yMovement) { throw new NotImplementedException(); } // this is the most complicated part, I don't want to completely just copy what I had right now
}
