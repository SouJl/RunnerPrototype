using Runner.Scripts.Enums;
using Runner.Scripts.Tool;
using Runner.Scripts.View;
using UnityEngine;

namespace Runner.Scripts.Game 
{
    internal class InputController : BaseController
    {
        private readonly ResourcePath _viewPath;

        private BaseInputView _view;

        public InputController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove, 
            PlayerModel player, InputType inputType) 
        {
            _viewPath = new ResourcePath($"Prefabs/{inputType.GetDescription()}");

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

