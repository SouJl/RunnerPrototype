using Runner.Features.AbilitySystem;
using Runner.Interfaces;
using Runner.Profile;
using Runner.Tool;
using Runner.Services;
using UnityEngine;

namespace Runner.Game
{
    internal class GameController:BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly PlayerController _playerController;
        private readonly InputController _inputController;
        private readonly AbilitiesContext _abilitiesContextController;
        private readonly TapeBackGroundgontroller _tapeBackgroundController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();


            _playerController = CreatePlayerController(profilePlayer);
            _inputController = CreateInputController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesContextController = CreateAbilitiesContext(placeForUi, _playerController);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff); 

            ServicesHandler.Analytics.SendGameStarted(profilePlayer.InputType.GetDescription());
        }

        private PlayerController CreatePlayerController(ProfilePlayer profilePlayer)
        {
            var playerController = new PlayerController(profilePlayer.Player);
            AddController(playerController);

            return playerController;
        }

        private InputController CreateInputController(
            ProfilePlayer profilePlayer,
            SubscriptionProperty<float> leftMoveDiff, 
            SubscriptionProperty<float> rightMoveDiff)
        {
            var inputController = new InputController(
                leftMoveDiff, rightMoveDiff, 
                profilePlayer.Player, profilePlayer.InputType);

            AddController(inputController);

            return inputController;
        }
        private AbilitiesContext CreateAbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            var abilitiesContextController = new AbilitiesContext(placeForUi, abilityActivator);
            AddContext(abilitiesContextController);

            return abilitiesContextController;
        }

        private TapeBackGroundgontroller CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var tapeBackgroundController = new TapeBackGroundgontroller(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }      
     
    }
}
