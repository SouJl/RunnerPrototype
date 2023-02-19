using Features.Storage.Upgrade;

namespace Runner.Scripts.Game 
{
    internal class PlayerModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _defaultJumpHeight;

        public float Speed { get; set; }
        public float JumpHeight { get; set; }

        public PlayerModel(float speed, float jumpHeight)
        {
            _defaultSpeed = speed;
            Speed = speed;

            _defaultJumpHeight = jumpHeight;
            JumpHeight = jumpHeight;
        }
 
        public void Restore()
        {
            Speed = _defaultSpeed;
            JumpHeight = _defaultJumpHeight;
        }
    }
}

