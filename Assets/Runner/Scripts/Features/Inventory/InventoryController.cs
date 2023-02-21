using Features.Inventory.Items;
using Runner;
using System;
using JetBrains.Annotations;
using Features.Decription;

namespace Features.Inventory
{
    internal interface IInventoryController
    {

    }

    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly IInventoryView _view;
        private readonly IInventoryModel _model;
        private readonly IItemsRepository _itemsRepository;
        private readonly IDescriptionController _descriptionController;
       
        public InventoryController(
            [NotNull] IInventoryModel inventoryModel,
            [NotNull] IInventoryView view,
            [NotNull] IItemsRepository itemsRepository,
            [NotNull] IDescriptionController descriptionController)
        {
            _model 
                = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
            _view 
                = view ?? throw new ArgumentNullException(nameof(view));
            _itemsRepository
                = itemsRepository ?? throw new ArgumentNullException(nameof(itemsRepository));
            _descriptionController
                = descriptionController ?? throw new ArgumentNullException(nameof(descriptionController));

            _view.Display(_itemsRepository.Items.Values, OnItemClicked);

            foreach (string itemId in _model.EquippedItems)
                _view.Select(itemId);
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
