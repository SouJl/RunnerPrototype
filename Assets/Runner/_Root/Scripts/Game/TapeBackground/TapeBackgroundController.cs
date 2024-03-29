﻿using Runner.Interfaces;
using Runner.Tool;
using UnityEngine;

namespace Runner.Game
{
    internal class TapeBackgroundController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Background/TapeBackground");

        private readonly SubscriptionProperty<float> _diff;
        private readonly ISubscriptionProperty<float> _leftMove;
        private readonly ISubscriptionProperty<float> _rightMove;

        private TapeBackgroundView _view;

        public TapeBackgroundController(
            SubscriptionProperty<float> leftMove, 
            SubscriptionProperty<float> rightMove) 
        {
            _diff = new SubscriptionProperty<float>();
            _leftMove = leftMove;
            _rightMove = rightMove;

            _view = LoadView();
            _view.Init(_diff);

            _leftMove.SubscriptionOnChange(MoveLeft);
            _rightMove.SubscriptionOnChange(MoveRight);
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscriptionOnChange(MoveLeft);
            _rightMove.UnSubscriptionOnChange(MoveRight);
        }

        private TapeBackgroundView LoadView() 
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<TapeBackgroundView>();
        }


        private void MoveLeft(float value) => _diff.Value = -value;

        private void MoveRight(float value) => _diff.Value = value;

    }
}
