using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters.Scripts.PhysicsCode
{
	public class GoombaMovement : EnemyPhysics
	{
		private float horizontalSpeed = 2f;
		//public Rigidbody2D rb;
		bool facingLeft;
    	// Update is called once per frame
		protected override void enemyMovement()
    	{
	        if (IsGrounded)
	        {
		        
		        TargetVelocity.x = horizontalSpeed;
	        }
        }

		public void reverseDirection()
		{
			horizontalSpeed *= -1;
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			//Debug.Log(collider.gameObject.layer);
			if (collider.gameObject.layer == 14)
			{
				horizontalSpeed *= -1;
				Debug.Log("hello");
			}
				
		}
	}
}