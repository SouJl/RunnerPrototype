using Features.AbilitySystem;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Profile;
using Runner.Scripts.Tool;
using Runner.Services;
using System;
using UnityEngine;

namespace Runner.Scripts.Game
{
    internal class GameController:BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly PlayerController _playerController;
        private readonly InputController _inputController;
        private readonly AbilitiesController _abilitiesController;
        private readonly TapeBackGroundgontroller _tapeBackgroundController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();


            _playerController = CreatePlayerController();
            _inputController = CreateInputController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesController = CreateAbilitiesController(placeForUi, _playerController);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff); 

            ServicesHandler.Analytics.SendGameStarted(profilePlayer.InputType.GetDescription());
        }

        private PlayerController CreatePlayerController()
        {
            var playerController = new PlayerController();
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
        private AbilitiesController CreateAbilitiesController(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            var abilitiesController = new AbilitiesController(placeForUi, abilityActivator);
            AddController(abilitiesController);

            return abilitiesController;
        }

        private TapeBackGroundgontroller CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var tapeBackgroundController = new TapeBackGroundgontroller(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }      
    }
}
