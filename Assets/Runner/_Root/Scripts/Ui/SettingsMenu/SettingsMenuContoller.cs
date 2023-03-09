using Runner.Profile;
using Runner.Tool;
using UnityEngine;

namespace Runner.UI
{
    internal class SettingsMenuContoller : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/UI/SettingsMenuUi");
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
