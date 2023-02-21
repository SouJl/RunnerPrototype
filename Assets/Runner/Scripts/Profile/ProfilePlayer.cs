using Runner.Tool;
using Runner.Enums;
using Runner.Game;
using Features.Inventory;

namespace Runner.Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InputType InputType;
        public readonly PlayerModel Player;
        public readonly InventoryModel Inventory;

        public ProfilePlayer(InputType inputType, 
            float speed, 
            float jumpHeight,
            float health, 
            GameState initialState) : this(inputType, speed, jumpHeight, health)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(InputType inputType, float speed, float jumpHeight, float health)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            InputType = inputType;
            Player = new PlayerModel(speed, jumpHeight, health);
            Inventory = new InventoryModel();
        }
    }
}
