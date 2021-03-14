using System;
using Characters.Scripts.PhysicsCode;
using UnityEngine;

public class GroundNormalTester: MonoBehaviour

{
    public EnemyPhysics enemyPhysics;
    //private Vector2 groundNormal => enemyPhysics.GroundNormal ;
    //private float angle => (float)Math.Atan(enemyPhysics.GroundNormal.y/enemyPhysics.GroundNormal.x)*Mathf.Rad2Deg;
    private void Update()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //throw new NotImplementedException();
    }

    private void getVectorAngle()
    {
        
    }
}