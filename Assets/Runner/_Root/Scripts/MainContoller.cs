﻿using Runner.Features.Reward;
using Runner.Features.Storage;
using Runner.Game;
using Runner.Profile;
using Runner.Services;
using Runner.UI;
using UnityEngine;

namespace Runner
{
    internal class MainContoller : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;

        private MainMenuController _mainMenuController;
        private SettingsMenuContoller _settingsMenuContoller;
        private RewardController _rewardController; 
        private GameController _gameController;
        private StorageContext _storageContext;

        public MainContoller(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _profilePlayer.CurrentState.SubscriptionOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        protected override void OnDispose()
        {
            DisposeControllers();
            _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state) 
        {
            DisposeControllers();

            switch (state) 
            {

                case GameState.Start: 
                    {
                        _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                        break;
                    }
                case GameState.Settings: 
                    {
                        _settingsMenuContoller = new SettingsMenuContoller(_placeForUi, _profilePlayer);
                        break;
                    }
                case GameState.Storage: 
                    {
                        _storageContext = new StorageContext(_placeForUi, _profilePlayer);
                        break;
                    }
                case GameState.DailyReward:
                    {
                        _rewardController = new RewardController(_placeForUi, _profilePlayer);
                        break;
                    }
                case GameState.Game: 
                    {
                        _gameController = new GameController(_placeForUi, _profilePlayer);
                        break;
                    }
                case GameState.Exit: 
                    {
                        Exit();
                        break;
                    }
            }
        }


        private void DisposeControllers()
        {
            _mainMenuController?.Dispose();
            _settingsMenuContoller?.Dispose();
            _gameController?.Dispose();
            _storageContext?.Dispose();
            _rewardController?.Dispose();
        }


        private void Exit()
        {
            ServicesHandler.PushNotification.CreateNotificationById("gameEnd_id");
            Application.Quit();
        }
    }
}
