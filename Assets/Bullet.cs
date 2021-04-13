using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 10f;
    // Rigidbody2D of the bullet
    public Rigidbody2D rb;
    // the player's coordinates
    private Transform player;
    // the enemy's coordinates
    private Transform enemy;
    // Vector2 for the speed of the Bullet
    private Vector2 speed;
    void Start()
    {
        // initializes the coordinates of the player and enemy
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Transform>();
        // find the speed of the bullet based on the ratio between the coordinates of the player and enemy
        speed = player.position - enemy.position;
        speed.Normalize();
        rb.velocity = speed * bulletSpeed;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        // destroys the bullet if it hits anything other than the enemy itself
        if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
