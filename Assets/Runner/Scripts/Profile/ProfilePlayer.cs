using Runner.Tool;
using Runner.Game;
using Features.Inventory;

namespace Runner.Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InputType InputType;
        public readonly PlayerModel Player;
        public readonly IInventoryModel Inventory;

        public ProfilePlayer(IProfileData profileData)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentState.Value = profileData.GameState;
            InputType = profileData.InputType;

            Player = new PlayerModel(profileData.PlayerData);
            
            Inventory = new InventoryModel();
        }
    }
}
