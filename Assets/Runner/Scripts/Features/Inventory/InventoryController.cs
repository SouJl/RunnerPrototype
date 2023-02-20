using Features.Inventory.Items;
using Runner.Scripts;
using Runner.Scripts.Tool;
using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;
using Features.Decription;
using System.Collections.Generic;

namespace Features.Inventory
{
    internal interface IInventoryController
    {

    }

    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/InventoryView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Items/ItemsDataConfig");

        private readonly InventoryView _view;
        private readonly IInventoryModel _model;
        private readonly ItemsRepository _repository;
        private readonly IDescriptionController _descriptionController;
        public InventoryController(
            [NotNull] Transform placeForUi,
            [NotNull] IInventoryModel inventoryModel)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _model = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));

            _view = LoadView(placeForUi);
       
            _repository = CreateRepository();
            _descriptionController = CreateDescription(_view.PlaceForDescription, _repository.Items.Values);

            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemId in _model.EquippedItems)
                _view.Select(itemId);

        }

        private InventoryView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private void OnItemClicked(string itemId)
        {
            bool equipped = _model.IsEquipped(itemId);

            if (equipped)
                UnequipItem(itemId);
            else
                EquipItem(itemId);

            _descriptionController.Show(itemId);
        }

        private ItemsRepository CreateRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfig(_dataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }


        private IDescriptionController CreateDescription(Transform placeForDescription, IEnumerable<IItem> values)
        {
            DecriptionController controller = new(placeForDescription, values);
            AddController(controller);

            return controller;
        }


        private void EquipItem(string itemId)
        {
            _view.Select(itemId);
            _model.EquipItem(itemId);
        }

        private void UnequipItem(string itemId)
        {
            _view.Unselect(itemId);
            _model.UnequipItem(itemId);
        }
    }
}
