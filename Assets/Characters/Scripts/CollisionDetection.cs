using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Scripts;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public EnemyPhysics enemyPhysics;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyPhysics.EnterBulletTime();
        //Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemyPhysics.ExitBulletTime();
        //Debug.Log("this actually works");
        //throw new NotImplementedException();
    }
}
