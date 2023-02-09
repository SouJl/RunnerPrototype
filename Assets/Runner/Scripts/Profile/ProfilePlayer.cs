using Runner.Scripts.Tool;
using Runner.Scripts.Enums;
using Runner.Scripts.Game;

namespace Runner.Scripts.Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InputType InputType;
        public readonly PlayerModel Player;

        public ProfilePlayer(InputType inputType, float speed, GameState initialState) : this(inputType, speed)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(InputType inputType, float speed)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            InputType = inputType;
            Player = new PlayerModel(speed);
        }
    }
}
