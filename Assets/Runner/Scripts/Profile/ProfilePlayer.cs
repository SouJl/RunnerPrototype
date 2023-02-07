using Runner.Scripts.Tool;
using Runner.Scripts.Enums;
using Runner.Scripts.Game;

namespace Runner.Scripts.Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly PlayerModel Player;

        public ProfilePlayer(float speed, GameState initialState) : this(speed)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float speed)
        {
            Player = new PlayerModel(speed);
        }
    }
}
