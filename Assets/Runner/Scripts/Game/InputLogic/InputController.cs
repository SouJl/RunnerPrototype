using Runner.Scripts.Tool;
using Runner.Scripts.View;
using UnityEngine;

namespace Runner.Scripts.Game 
{
    internal class InputController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/EndlessMove");

        private BaseInputView _view;

        public InputController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove, 
            PlayerModel player) 
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, player.Speed);
        }

        private BaseInputView LoadView()
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<BaseInputView>();
        }
    }
}

