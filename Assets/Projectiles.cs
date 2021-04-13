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
    public GameObject bulletPrefab;
    void Update()
    {
        if (timeBetweenShots <= 0)
        {
            Shoot();
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
