using Runner.Scripts.Enums;
using Runner.Scripts.Game;
using Runner.Scripts.Profile;
using Runner.Scripts.Ui;
using UnityEngine;

namespace Runner.Scripts
{
    internal class MainContoller : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;

        private MainMenuController _mainMenuController;
        private SettingsMenuContoller _settingsMenuContoller;
        private GameController _gameController;

        public MainContoller(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            _profilePlayer.CurrentState.SubscriptionOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        protected override void OnDispose()
        {
            _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state) 
        {
            switch (state) 
            {
                default: 
                    {
                        _mainMenuController?.Dispose();
                        _settingsMenuContoller?.Dispose();
                        _gameController?.Dispose();
                        break;
                    }
                case GameState.Start: 
                    {
                        _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                        _gameController?.Dispose();
                        _settingsMenuContoller?.Dispose();
                        break;
                    }
                case GameState.Settings: 
                    {
                        _settingsMenuContoller = new SettingsMenuContoller(_placeForUi, _profilePlayer);
                        _mainMenuController?.Dispose();
                        _gameController?.Dispose();
                        break;
                    }
                case GameState.Game: 
                    {
                        _gameController = new GameController(_profilePlayer);
                        _mainMenuController?.Dispose();
                        _settingsMenuContoller?.Dispose();
                        break;
                    }
                case GameState.Exit: 
                    {

                        _mainMenuController?.Dispose();
                        _settingsMenuContoller?.Dispose();
                        _gameController?.Dispose();
                        Application.Quit();
                        break;
                    }
            }
        }

    }
}
