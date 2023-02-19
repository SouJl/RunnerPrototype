using Features.Inventory.Items;
using Runner.Scripts;
using Runner.Scripts.Tool;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Features.Decription
{
    internal interface IDescriptionController 
    {
        void Show(string id);
        void Hide(string id);
    }

    internal class DecriptionController : BaseController, IDescriptionController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/DescriptionView");

        private readonly DescriptionView _view;

        public DecriptionController(
            [NotNull] Transform placeForUi,
            [NotNull] IEnumerable<IItem> itemsCollection) 
        {
            if (!placeForUi)
                throw new ArgumentNullException(nameof(placeForUi));

            _view = LoadView(placeForUi);
            _view.Init(itemsCollection);
        }

        private DescriptionView LoadView(Transform placeForUi)
        {

            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<DescriptionView>();
        }

        public void Show(string id) =>
            _view.Show(id);

        public void Hide(string id) =>
            _view.Hide(id);
    }
}
