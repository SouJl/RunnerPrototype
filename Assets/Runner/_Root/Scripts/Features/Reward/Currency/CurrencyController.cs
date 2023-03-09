using Runner.Tool;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.Features.Reward
{
    internal class CurrencyController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Rewards/CurrencyView");
        private readonly ICurrencyModel _model;
        private readonly ICurrencyView _view;

        public CurrencyController(Transform placeForUi, 
            [NotNull] ICurrencyModel model)
        {
            _model
                = model ?? throw new ArgumentNullException(nameof(model));
            _view = LoadView(placeForUi);
            _view.Init(_model.Scheme, model.Diamond);

            Subscribe(_model);
        }

        protected override void OnDispose()
        {
            Unsubscribe(_model);
            base.OnDispose();
        }

        private ICurrencyView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CurrencyView>();
        }

        private void Subscribe(ICurrencyModel model)
        {
            model.SchemeChanged += OnShemeChanged;
            model.DiamondChanged += OnDiamondChanged;
        }

        private void Unsubscribe(ICurrencyModel model)
        {
            model.SchemeChanged -= OnShemeChanged;
            model.DiamondChanged -= OnDiamondChanged;
        }

        private void OnShemeChanged() => _view.AddScheme(_model.Scheme);
        private void OnDiamondChanged() => _view.AddDiamond(_model.Diamond);
    }
}
