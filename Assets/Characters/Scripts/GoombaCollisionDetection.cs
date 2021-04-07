using System.Collections;
using System.Collections.Generic;
using System.Text;
using Characters.Scripts;
using Characters.Scripts.PhysicsCode;
using UnityEngine;

public class GoombaCollisionDetection : CollisionDetection
{
    public EnemyMovement enemyMovement;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            enemyMovement.reverseDirection();
        }
        base.OnTriggerEnter2D(collision);
    }
}
