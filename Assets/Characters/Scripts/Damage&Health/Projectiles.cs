using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    // Transform for where the bullets are released from the enemy
    public Transform firePoint;
    // Time in seconds between when each bullet is fired
    public float startTimeBetweenShots;
    // Countdown for when the next bullet will be fired. When it reaches 0, the next bullet is fired
    // and it resets to startTimeBetweenShots.
    private float timeBetweenShots;
    public bool isGrounded = false;
    // the player's coordinates
    private Transform player;
    // the enemy's coordinates
    private Transform enemy;
    public GameObject bulletPrefab;
    public Vector2 angleTowardsPlayer;
    public float visibilityDistance;
    private void Start()
    {
        Debug.Log("updated! :)");
        timeBetweenShots = startTimeBetweenShots;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Transform>();
    }

    bool canSeePlayer(float distance)
    {
        angleTowardsPlayer = player.position - enemy.position;
        angleTowardsPlayer.Normalize();

        var castDistance = distance;
        Vector2 endPosition = enemy.position + ((Vector3) angleTowardsPlayer * distance);
        RaycastHit2D hitPlayer = Physics2D.Linecast(enemy.position, endPosition, 1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D hitWall = Physics2D.Linecast(enemy.position, endPosition, 1 << LayerMask.NameToLayer("Default"));
        if (hitPlayer.collider != null && hitWall.collider != null)
        {
            if (Vector2.Distance(hitPlayer.collider.gameObject.transform.position, enemy.position)
                < Vector2.Distance(hitWall.collider.gameObject.transform.position, enemy.position))
            {
                return true;
            }
        }

        return false;

}
    void Update()
    {
        if (timeBetweenShots <= 0 && isGrounded)
        {
            if (canSeePlayer(visibilityDistance))
            {
                Shoot();
                timeBetweenShots = startTimeBetweenShots;
            }
        }
        else
        {
            timeBetweenShots -= Time.fixedDeltaTime;
        }
    }

    void Shoot()
    {
        
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
