using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public float rocketSpeed = 15f;

    public Rigidbody2D rb;
    private Transform playerTransform;
    private Transform enemy;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("do something");
        //rb = gameObject.GetComponent<Rigidbody2D>();
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Transform>();
        // find the speed of the bullet based on the ratio between the coordinates of the player and enemy
        var vectorTowardsPlayer = player.position - enemy.position;
        var angleTowardsPlayer = Mathf.Atan2(vectorTowardsPlayer.y, vectorTowardsPlayer.x) * Mathf.Rad2Deg;
        vectorTowardsPlayer.Normalize(); //Makes it so the vector is always the same size each time
        enemy.rotation = Quaternion.Euler(new Vector3(0, 0, angleTowardsPlayer)); //This theoretically should angle the rocket correctly
        GetComponent<Rigidbody2D>().velocity = vectorTowardsPlayer * rocketSpeed;
        //Debug.Log(vectorTowardsPlayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11) return;
        Destroy(gameObject);
        
        //throw new NotImplementedException();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        var currentAngle= GetMouseAngle.MouseAngle(player);
        Debug.Log("mouse angle: "+currentAngle);
            
        GetComponent<Rigidbody2D>().velocity = new Vector2((float)Math.Cos(currentAngle*Mathf.Deg2Rad)*rocketSpeed, //*Mathf.Rad2Deg
            (float)Math.Sin(currentAngle*Mathf.Deg2Rad)*rocketSpeed);
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
        //throw new NotImplementedException();
    }

    // Update is called once per frame
    
}
