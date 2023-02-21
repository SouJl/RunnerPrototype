using Features.Inventory;
using Features.Storage.Upgrade;
using JetBrains.Annotations;
using Runner.Scripts;
using Runner.Scripts.Enums;
using Runner.Scripts.Profile;
using System;
using System.Collections.Generic;

namespace Features.Storage
{
    internal interface IStorageController
    {
    }

    internal class StorageController : BaseController, IStorageController
    {
       

        private readonly IStorageView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly BaseContext _inventoryContext;


        public StorageController(
            [NotNull] IStorageView view,
            [NotNull] ProfilePlayer profilePlayer,
            [NotNull] IUpgradeHandlersRepository upgradeHandlersRepository,
            [NotNull] BaseContext inventoryContext)
        {
            _view 
                = view ?? throw new ArgumentNullException(nameof(view));
            _profilePlayer 
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));
            _upgradeHandlersRepository 
                = upgradeHandlersRepository ?? throw new ArgumentNullException(nameof(upgradeHandlersRepository));
            _inventoryContext 
                = inventoryContext ?? throw new ArgumentNullException(nameof(inventoryContext));
            
            _view.Init(Apply, Back); 
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
