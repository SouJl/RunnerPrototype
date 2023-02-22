using Features.Storage;
using Runner.Game;
using Runner.Profile;
using Runner.Ui;
using UnityEngine;

namespace Runner
{
    internal class MainContoller : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;

        private MainMenuController _mainMenuController;
        private SettingsMenuContoller _settingsMenuContoller;
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
                case GameState.Game: 
                    {
                        _gameController = new GameController(_placeForUi, _profilePlayer);
                        break;
                    }
                case GameState.Exit: 
                    {
                        Application.Quit();
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
        }

    }
}
