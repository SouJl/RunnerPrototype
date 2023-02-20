using Features.Storage.Upgrade;

namespace Runner.Scripts.Game 
{
    internal class PlayerModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _defaultJumpHeight;
        private readonly float _defaultHealth;

        public float Speed { get; set; }
        public float JumpHeight { get; set; }

        public float Health { get; set; }
        public PlayerModel(float speed, float jumpHeight, float health)
        {
            _defaultSpeed = speed;
            Speed = speed;

            _defaultJumpHeight = jumpHeight;
            JumpHeight = jumpHeight;

            _defaultHealth = health;
            Health = health;
        }
 
        public void Restore()
        {
            Speed = _defaultSpeed;
            JumpHeight = _defaultJumpHeight;
            Health = _defaultHealth;
        }
    }
}

