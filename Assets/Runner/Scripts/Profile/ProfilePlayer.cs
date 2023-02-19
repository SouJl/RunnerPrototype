using Runner.Scripts.Tool;
using Runner.Scripts.Enums;
using Runner.Scripts.Game;
using Features.Inventory;

namespace Runner.Scripts.Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InputType InputType;
        public readonly PlayerModel Player;
        public readonly InventoryModel Inventory;

        public ProfilePlayer(InputType inputType, float speed, float jumpHeight, GameState initialState) : this(inputType, speed, jumpHeight)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(InputType inputType, float speed, float jumpHeight)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            InputType = inputType;
            Player = new PlayerModel(speed, jumpHeight);
            Inventory = new InventoryModel();
        }
    }
}
