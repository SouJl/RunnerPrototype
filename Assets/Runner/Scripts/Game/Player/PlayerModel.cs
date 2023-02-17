using Features.Storage.Upgrade;

namespace Runner.Scripts.Game 
{
    internal class PlayerModel : IUpgradable
    {
        public readonly float _defaultSpeed;

        public float Speed { get; set; }

        public PlayerModel(float speed)
        {
            _defaultSpeed = speed;
            Speed = speed;
        }
 
        public void Restore()
        {
            Speed = _defaultSpeed;
        }
    }
}

