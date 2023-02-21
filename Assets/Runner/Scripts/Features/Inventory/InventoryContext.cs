using Runner;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using System;
using Runner.Tool;
using Features.Inventory.Items;
using Object = UnityEngine.Object;
using Features.Decription;
using System.Collections.Generic;

namespace Features.Inventory
{
    internal class InventoryContext : BaseContext
    {
        private readonly ResourcePath _dataSourcePath = new("Configs/Items/ItemsDataConfig");
        private readonly ResourcePath _viewPath = new("Prefabs/InventoryView");

        public InventoryContext(
            [NotNull] Transform placeForUi, 
            [NotNull] IInventoryModel inventoryModel) 
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));
            if (inventoryModel == null)
                throw new ArgumentNullException(nameof(inventoryModel));

            CreateController(placeForUi, inventoryModel);
        }

        private void CreateController(Transform placeForUi, IInventoryModel inventoryModel)
        {
            ItemConfig[] itemsConfigs = LoadConfigs();
            InventoryView view = CreateView(placeForUi);
            ItemsRepository repository = CreateRepository(itemsConfigs);
            IDescriptionController descriptionController = CreateDescription(view.PlaceForDescription, repository.Items.Values);

            var controller = new InventoryController(inventoryModel, view, repository, descriptionController);

            AddController(controller);
        }

        private ItemConfig[] LoadConfigs() =>
           ContentDataSourceLoader.LoadItemConfig(_dataSourcePath);

        private InventoryView CreateView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private ItemsRepository CreateRepository(ItemConfig[] itemConfigs)
        {
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }

        private IDescriptionController CreateDescription(Transform placeForUi, IEnumerable<IItem> items)
        {
            DecriptionController descriptionController = new(placeForUi, items);
            AddController(descriptionController);

            return descriptionController;
        }
    }
}
