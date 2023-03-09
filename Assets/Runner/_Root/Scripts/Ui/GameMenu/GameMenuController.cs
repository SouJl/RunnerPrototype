using Runner.Profile;
using Runner.Tool;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.UI
{
    internal class GameMenuController :BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/UI/GameMenuView");

        private readonly ProfilePlayer _profilePlayer;
        private readonly IGameMenuView _view;
        private readonly IPauseMenuModel _pauseMenuModel;

        public GameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer 
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));
            _view = LoadView(placeForUi);
            _view.Init(ToMainMenu, OpenPauseMenu);

            _pauseMenuModel = new PauseMenuModel();
            CreatePauseMenuController(placeForUi, profilePlayer, _pauseMenuModel);
        }

        private IGameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameMenuView>();
        }

        private PauseMenuController CreatePauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IPauseMenuModel pauseMenuModel)
        {
            var pauseMenuController 
                = new PauseMenuController(placeForUi, profilePlayer, pauseMenuModel);
            AddController(pauseMenuController);

            return pauseMenuController;
        }

        private void ToMainMenu() => _profilePlayer.CurrentState.Value = GameState.Start;

        private void OpenPauseMenu() => _pauseMenuModel.Enable();


    }
}
