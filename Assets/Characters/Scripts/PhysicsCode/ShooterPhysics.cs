namespace Characters.Scripts.PhysicsCode
{
    public class ShooterPhysics: EnemyPhysics
    {
        public Projectiles projectiles;
        // ReSharper disable once InconsistentNaming
        private bool wasGrounded;
        protected override void GroundedCheck() 
        /*
         * This just updates the grounded boolean in the projectiles script
         * Seems like a bad way to do this
         */
         
        {
            if (IsGrounded != wasGrounded)
            {
                projectiles.isGrounded = IsGrounded;
                wasGrounded = IsGrounded;
            }
        }
    }
}