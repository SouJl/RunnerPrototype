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
        private readonly InventoryController _inventoryController;
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
            _inventoryController = CreateInventoryController(_view.InventoryPlaceUi);
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryController CreateInventoryController(Transform placeForUi)
        {
            var inventoryController = new InventoryController(placeForUi, _profilePlayer.Inventory);
            AddController(inventoryController);

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
            Log($"Apply. Current Player stats: " +
                $"\n Speed - > {_profilePlayer.Player.Speed}; JumpHight - > {_profilePlayer.Player.JumpHeight}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Current Player stats: " +
                 $"\n Speed - > {_profilePlayer.Player.Speed}; JumpHight - > {_profilePlayer.Player.JumpHeight}");
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

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
