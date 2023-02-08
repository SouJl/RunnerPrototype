using Runner.Scripts.Enums;
using Runner.Scripts.Profile;
using Runner.Scripts.Tool;
using Runner.Scripts.View;
using UnityEngine;

namespace Runner.Scripts.Ui
{
    internal class SettingsMenuContoller : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath
        {
            PathResource = "Prefabs/Resources/mainMenu"
        };

        private ProfilePlayer _profilePlayer;
        private SettingsMenuView _view;


        public SettingsMenuContoller(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _view = LoadView(placeForUi);
            _view.Init(BackToMenu);
        }

        private SettingsMenuView LoadView(Transform placeForUi)
        {
            GameObject objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
            AddGameObject(objectView);
            return objectView.GetComponent<SettingsMenuView>();
        }


        private void BackToMenu()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }
    }
}
