using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Scripts;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    //public string attackKey = "p";
    public int attackDamage = 10;
    public float cooldownTime = 1f;
    private bool _isCooldown = false;

    public BoxCollider2D attackCollider;

    public SpriteRenderer hitboxSprite;

    public PlayerController playerPhysics;
    // Start is called before the first frame update
    void Start()
    {
        //attackCollider = GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
        hitboxSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && !_isCooldown)
        {
            attackCollider.enabled = true;
            hitboxSprite.enabled = true;
            //Debug.Log("The Collider has been enabled");
        }
        else if(attackCollider.enabled)
        {
            attackCollider.enabled = false;
            hitboxSprite.enabled = false;
            //Debug.Log("The Collider has been disabled");
        }
    }
    
    private IEnumerator Cooldown()
    {
        // Start cooldown
        _isCooldown = true;
        // Wait for time you want
        yield return new WaitForSeconds(cooldownTime);
        // Stop cooldown
        _isCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerPhysics.EnterBulletTime();
        //Debug.Log("hit the enemy");
        //throw new NotImplementedException();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerPhysics.ExitBulletTime();
        //throw new NotImplementedException();
    }
}
