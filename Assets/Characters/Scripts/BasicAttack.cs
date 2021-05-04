﻿using System;
using System.Collections;
using System.Net.Mail;
using Characters.Scripts.PhysicsCode;
using UnityEngine;

namespace Characters.Scripts
{
    public class BasicAttack : MonoBehaviour
    {
        //public string attackKey = "p";
        public int attackDamage = 10;
        public float cooldownTime = 1f;
        public float attackLength = 5f;

        private float _timeLeftInAttack = -1; //set at -1 so that the hitbox comes out on first click, (first if statement in update function runs without touching anybody otherwise)

        private bool _isCooldown = false;

        public BoxCollider2D attackCollider;
        public SpriteRenderer hitboxSprite;
        public PlayerController playerPhysics;
        public TextMesh attackText;
        
        // Start is called before the first frame update
        void Start()
        {
            //attackCollider = GetComponent<BoxCollider2D>();
            attackCollider.enabled = false;
            hitboxSprite.enabled = false;
            attackText.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetMouseButton(0)&& !_isCooldown)
            {
                
                double roundedTimeLeftInAttack = 0;
                if (_timeLeftInAttack >= 0)
                {
                    Debug.Log("hello");
                    attackText.gameObject.SetActive(true);
                    _timeLeftInAttack -= 0.02f;
                    roundedTimeLeftInAttack = Math.Round(_timeLeftInAttack,2);
                }
                
                if (roundedTimeLeftInAttack < 0)
                {
                    attackText.gameObject.SetActive(false);
                    _timeLeftInAttack = -1;
                    var cooldown = Cooldown();
                    StartCoroutine(cooldown);
                    return;
                }
                attackText.text = roundedTimeLeftInAttack.ToString();
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
            _timeLeftInAttack = attackLength;
            //Debug.Log(other.name);
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
}
