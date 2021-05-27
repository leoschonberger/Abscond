using System.Collections;
using System.Collections.Generic;
using Characters.Scripts;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Health HealthScript;
    private int Health => HealthScript.currentHp;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Health", Health);
    } 
    
}
