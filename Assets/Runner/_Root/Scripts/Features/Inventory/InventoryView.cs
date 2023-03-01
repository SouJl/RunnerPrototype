using Runner.Features.Inventory.Items;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Features.Inventory
{
    internal interface IInventoryView
    {
        void Display(IEnumerable<IItem> itemsCollection, Action<string> itemClicked);
        void Clear();
        void Select(string id);
        void Unselect(string id);
    }

    internal class InventoryView : MonoBehaviour, IInventoryView
    {

        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _placeForItems;
        [SerializeField] private Transform _placeForDescription;

        private readonly Dictionary<string, ItemView> _itemViews = new Dictionary<string, ItemView>();

        public Transform PlaceForDescription => _placeForDescription;

        public void Display(IEnumerable<IItem> itemsCollection, Action<string> itemClicked)
        {
            Clear();

            foreach (var item in itemsCollection) 
            {
                _itemViews[item.Id] = CreateItemView(item, itemClicked);
            }      
        }

        private void OnDestroy() => Clear();

        public void Clear()
        {
            foreach (var itemView in _itemViews.Values)
                DestroyItemView(itemView);

            _itemViews.Clear();
        }

        public void Select(string id)
            => _itemViews[id].Select();

        public void Unselect(string id)
            => _itemViews[id].Unselect();

        private ItemView CreateItemView(IItem item, Action<string> itemClicked)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, _placeForItems, false);
            ItemView itemView = objectView.GetComponent<ItemView>();

            itemView.Init
            (
                item,
                () => itemClicked?.Invoke(item.Id)
            );

            return itemView;
        }

        private void DestroyItemView(ItemView itemView)
        {
            itemView.Deinit();
            Destroy(itemView.gameObject);
        }
    }
}
