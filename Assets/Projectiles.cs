using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;

    public GameObject bulletPrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
