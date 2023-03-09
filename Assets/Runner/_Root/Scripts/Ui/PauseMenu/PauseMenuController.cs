using Runner.Profile;
using Runner.Tool;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.UI
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/UI/PauseMenuView");
        
        private readonly ProfilePlayer _profilePlayer;
        private readonly IPauseMenuModel _model;
        private readonly IPauseMenuView _view;

        public PauseMenuController(
            Transform placeForUi, 
            ProfilePlayer profilePlayer,
            IPauseMenuModel model)
        {
            _profilePlayer 
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));
            _model
                = model ?? throw new ArgumentNullException(nameof(model));

            Subscribe(_model);

            _view = LoadView(placeForUi);
            _view.Init(Resume, ToMainMenu);

            if (_model.IsEnabled) _view.Show();
            else _view.Hide();
        }

        protected override void OnDispose()
        {
            Unsubscribe(_model);
            base.OnDispose();
        }

        private void Subscribe(IPauseMenuModel model)
        {
            model.Enabled += OnPauseEnabled;
            model.Disabled += OnPauseDisabled;
        }

        private void Unsubscribe(IPauseMenuModel model)
        {
            model.Enabled -= OnPauseEnabled;
            model.Disabled -= OnPauseDisabled;
        }

        private void OnPauseEnabled() => _view.Show();
        private void OnPauseDisabled() => _view.Hide();

  
        private IPauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }


        private void ToMainMenu() => _profilePlayer.CurrentState.Value = GameState.Start;

        private void Resume() => _model.Disable();

    }
}
