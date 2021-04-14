using System.Collections;
using System.Collections.Generic;
using System.Text;
using Characters.Scripts;
using Characters.Scripts.PhysicsCode;
using UnityEngine;
using UnityEngine.Serialization;

public class GoombaCollisionDetection : CollisionDetection
{
    [FormerlySerializedAs("enemyMovement")] public GoombaMovement goombaMovement;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            goombaMovement.reverseDirection();
        }
        base.OnTriggerEnter2D(collision);
    }
}
