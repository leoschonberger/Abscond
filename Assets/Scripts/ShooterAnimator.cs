using System.Collections;
using System.Collections.Generic;
using Characters.Scripts;
using UnityEngine;

public class ShooterAnimator : MonoBehaviour
{
    public Health healthScript;
    private int health => healthScript.currentHp;
    public Animator anim;
    public Projectiles attackLogic;
    private bool IsAttacking => attackLogic.IsAttacking;
    void Update()
    {
        anim.SetInteger("health",health);
        anim.SetBool("IsAttacking",IsAttacking);
    }
}
