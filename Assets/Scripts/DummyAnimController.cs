using System.Collections;
using System.Collections.Generic;
using Characters.Scripts;
using UnityEngine;

public class DummyAnimController : MonoBehaviour
{
    public EnemyHealth healthScript;
    private int health => healthScript.currentHealth;
    public Animator anim;
    private static readonly int Health = Animator.StringToHash("Health");

    void Update()
    {
        anim.SetInteger(Health,health);
    }
}
