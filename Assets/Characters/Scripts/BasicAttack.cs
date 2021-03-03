using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    //public string attackKey = "p";
    public int attackDamage = 10;
    public float cooldownTime = 1f;
    private bool isCooldown = false;

    public BoxCollider2D attackCollider;
    // Start is called before the first frame update
    void Start()
    {
        //attackCollider = GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && !isCooldown)
        {
            attackCollider.enabled = true;
            Debug.Log("The Collider has been enabled");
        }
        else if(attackCollider.enabled == true)
        {
            attackCollider.enabled = false;
            Debug.Log("The Collider has been disabled");
        }
    }
    
    private IEnumerator Cooldown()
    {
        // Start cooldown
        isCooldown = true;
        // Wait for time you want
        yield return new WaitForSeconds(cooldownTime);
        // Stop cooldown
        isCooldown = false;
    }
}
