using Features.Inventory;
using Features.Storage.Upgrade;
using JetBrains.Annotations;
using Runner.Scripts;
using Runner.Scripts.Enums;
using Runner.Scripts.Profile;
using Runner.Scripts.Tool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Storage
{
    internal interface IStorageController
    {
    }

    internal class StorageController : BaseController, IStorageController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/StorageView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Upgrades/UpgradeItemsDataConfig");

        private readonly StorageView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryContext _inventoryContext;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;


        public StorageController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (!placeForUi)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer 
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _view = LoadView(placeForUi);
            _view.Init(Apply, Back);


            _upgradeHandlersRepository = CreateRepository();
            _inventoryContext = CreateInventoryContext(_view.InventoryPlaceUi, _profilePlayer.Inventory); 
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            UpgradeHandlersRepository repository = new(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel inventoryModel)
        {
            InventoryContext inventoryController = new(placeForUi, inventoryModel);
            AddContext(inventoryController);

            return inventoryController;
        }

        private StorageView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<StorageView>();
        }

        private void Apply()
        {
            _profilePlayer.Player.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.Player,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            LogPlayerStats(_profilePlayer.Player);
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            LogPlayerStats(_profilePlayer.Player);
        }


        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }

        private void LogPlayerStats(IUpgradable upgradableUnit) =>
             Log($"Apply. Current Player stats: " +
               $"\n Speed - > {upgradableUnit.Speed}; " +
               $"JumpHight - > {upgradableUnit.JumpHeight};" +
               $"Helth - > {upgradableUnit.Health}");
    }
}
