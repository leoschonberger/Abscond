using System.Collections;
using System.Collections.Generic;
using Characters.Scripts.PhysicsCode;
using UnityEngine;


public class AnimationController : MonoBehaviour
{
   
    public PlayerController playerController;
    public bool facingRight = true;
    private Vector2 velocity => playerController.velocity;
    private bool IsGrounded => playerController.IsGrounded;
    public Animator anim;
    private bool IsMoving;
    private bool isAttacking;
    private bool isJumping;
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isAttacking = true;
            anim.SetBool("IsAttacking", isAttacking);

        }

        if (Input.GetMouseButton(0) == false)
        {
            isAttacking = false;
            anim.SetBool("IsAttacking", isAttacking);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("IsJumping", isJumping);
            
        }

        if (Input.GetKey(KeyCode.Space) == false)
        {
            isAttacking = false;
            anim.SetBool("IsJumping",isJumping);
        }
        
        if (velocity.x != 0.0)
        {
            IsMoving = true;
            if (facingRight == false && velocity.x > 0.0)
            {
                FlipCharacter();
            }
        }
        else
        {
            IsMoving = false;
        }
        anim.SetBool("IsGrounded", IsGrounded);
        anim.SetBool("IsMoving",IsMoving);

        if (velocity.x < 0.0)
        {
            if (facingRight)
            {
                FlipCharacter();
            }
        }

        
        
        
            
        
      
        void FlipCharacter()
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = !facingRight;
        }
        
        
        
        
    }
}




