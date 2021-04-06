using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters.Scripts.PhysicsCode
{
	public class EnemyMovement : EnemyPhysics
	{
		private float horizontalSpeed = 2f;
		public Rigidbody2D rb;
		bool facingLeft;
    	// Update is called once per frame
		protected override void enemyMovement()
    	{
	        if (IsGrounded)
	        {
		        if (velocity.x == 0)
		        {
			        horizontalSpeed *= -1;
			        Debug.Log(horizontalSpeed);
			        TargetVelocity.x = horizontalSpeed;
			        Debug.Log(TargetVelocity.x);
		        }
		        TargetVelocity.x = horizontalSpeed;
	        }
        }

		void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.gameObject.layer == 15)
				horizontalSpeed *= -1;
		}
	}
}