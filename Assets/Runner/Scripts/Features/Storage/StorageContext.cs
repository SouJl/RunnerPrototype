using Runner.Features.Inventory;
using Runner.Features.Storage.Upgrade;
using Runner;
using Runner.Profile;
using Runner.Tool;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.Features.Storage
{
    internal class StorageContext : BaseContext
    {

        private readonly ResourcePath _dataSourcePath = new("Configs/Upgrades/UpgradeItemsDataConfig");
        private readonly ResourcePath _viewPath = new("Prefabs/StorageView");

        public StorageContext(
            [NotNull] Transform placeForUi, 
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (profilePlayer == null)
                throw new ArgumentNullException(nameof(profilePlayer));

            CreateController(placeForUi, profilePlayer);
        }

        private void CreateController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            UpgradeItemConfig[] upgradeItemConfigs = LoadConfigs();
            UpgradeHandlersRepository upgradeHandlersRepository = CreateRepository(upgradeItemConfigs);
            StorageView view = CreateView(placeForUi);
            InventoryContext inventoryContext = CreateContext(view.InventoryPlaceUi, profilePlayer.Inventory);

            var controller = new StorageController(view, profilePlayer, upgradeHandlersRepository, inventoryContext);
            AddController(controller);
        }

        private UpgradeItemConfig[] LoadConfigs() =>
            ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);

        private UpgradeHandlersRepository CreateRepository(UpgradeItemConfig[] upgradeItemConfigs)
        {
            UpgradeHandlersRepository repository = new(upgradeItemConfigs);
            AddRepository(repository);

            return repository;
        }


        private StorageView CreateView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<StorageView>();
        }

        private InventoryContext CreateContext(Transform inventoryPlaceUi, IInventoryModel inventory)
        {
            var context = new InventoryContext(inventoryPlaceUi, inventory);
            AddContext(context);

            return context;
        }
    }
}
