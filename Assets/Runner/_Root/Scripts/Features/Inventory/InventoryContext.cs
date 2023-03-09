using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using System;
using Runner.Tool;
using Runner.Features.Inventory.Items;
using Runner.Features.Decription;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Runner.Features.Inventory
{
    internal class InventoryContext : BaseContext<ItemConfig, ItemsRepository, InventoryView>
    {
        private readonly ResourcePath _dataSourcePath = new("Configs/Items/ItemsDataConfig");
        private readonly ResourcePath _viewPath = new("Prefabs/Items/InventoryView");

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
            ItemsRepository repository = CreateRepository(itemsConfigs);
            InventoryView view = CreateView(placeForUi);
            IDescriptionController descriptionController = CreateDescription(view.PlaceForDescription, repository.Items.Values);

            var controller = new InventoryController(inventoryModel, view, repository, descriptionController);

            AddController(controller);
        }


        protected override ItemConfig[] LoadConfigs() =>
              ContentDataSourceLoader.LoadItemConfig(_dataSourcePath);

        protected override InventoryView CreateView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        protected override ItemsRepository CreateRepository(ItemConfig[] configsData)
        {
            var repository = new ItemsRepository(configsData);
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
