using Runner.Enums;
using Runner.Profile;
using Runner.Tool;
using Runner.View;
using UnityEngine;

namespace Runner.Ui
{
    internal class SettingsMenuContoller : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/SettingsMenuUi");
        private ProfilePlayer _profilePlayer;
        private SettingsMenuView _view;


        public SettingsMenuContoller(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _profilePlayer = profilePlayer;
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
