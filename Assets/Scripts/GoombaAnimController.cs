using System.Collections;
using System.Collections.Generic;
using Characters.Scripts;
using Characters.Scripts.PhysicsCode;
using UnityEngine;

public class GoombaAnimController : MonoBehaviour
{
    public Health healthScript;
    public GoombaMovement GoombaMovement;
    private bool IsMoving;
    private bool IsGrounded;
    private bool FacingRight;
    public Animator anim;
    private Vector2 velocity => GoombaMovement.velocity;
    private int health => healthScript.currentHp;
    void Update()
    {
        if (velocity.x != 0.0)
        {
            IsMoving = true;
            if (FacingRight == false && velocity.x > 0.0)
            {
                FlipCharacter();
            }
        }
        else
        {
            IsMoving = false;
        }
        anim.SetInteger("health",health);
        anim.SetBool("IsMoving",IsMoving);
    }
    void FlipCharacter()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        FacingRight = !FacingRight;
    }

}
