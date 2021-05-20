using System.Collections;
using System.Collections.Generic;
using Characters.Scripts;
using UnityEngine;

public class DummyAnimController : MonoBehaviour
{
    public Health healthScript;
    private int health => healthScript.currentHp;
    public Animator anim;
    
    void Update()
    {
        anim.SetInteger("health",health);
    }
}
