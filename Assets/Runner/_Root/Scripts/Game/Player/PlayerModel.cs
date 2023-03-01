using Runner.Features.Storage.Upgrade;
using Runner.Profile;

namespace Runner.Game 
{
    internal class PlayerModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _defaultJumpHeight;
        private readonly float _defaultHealth;

        public float Speed { get; set; }
        public float JumpHeight { get; set; }

        public float Health { get; set; }
        public PlayerModel(IPlayerData playerData)
        {
            _defaultSpeed = playerData.Speed;
            Speed = playerData.Speed;

            _defaultJumpHeight = playerData.JumpHeight;
            JumpHeight = playerData.JumpHeight;

            _defaultHealth = playerData.HealtPoints;
            Health = playerData.HealtPoints;
        }
 
        public void Restore()
        {
            Speed = _defaultSpeed;
            JumpHeight = _defaultJumpHeight;
            Health = _defaultHealth;
        }
    }
}

