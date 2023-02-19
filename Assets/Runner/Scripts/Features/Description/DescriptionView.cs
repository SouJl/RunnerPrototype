using Features.Inventory.Items;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Decription
{
    internal interface IDesctiprionView
    {
        void Init(IEnumerable<IItem> itemsCollection);

        void Clear();

        void Show(string id);
        void Hide(string id);
    }

    internal class DescriptionView : MonoBehaviour, IDesctiprionView
    {
        [SerializeField] private Image _descriptionIcon;
        [SerializeField] private Sprite DefaultIcon;
        [SerializeField] private TMP_Text _descriptionTitle;
        [SerializeField] private TMP_Text _descriptionInfo;

        private readonly Dictionary<string, ItemInfo> _itemsDecription = new Dictionary<string, ItemInfo>();

        public void Init(IEnumerable<IItem> itemsCollection)
        {
            Clear();

            foreach (var item in itemsCollection)
            {
                _itemsDecription[item.Id] = item.Info;
            }
        }

        private void OnDestroy() => Clear();

        public void Hide(string id)
        {
            SetDecriptionInfo(DefaultIcon, "", "");
        }

        public void Show(string id)
        {
            SetDecriptionInfo(_itemsDecription[id].Icon,  _itemsDecription[id].Title, _itemsDecription[id].Info);
        }

        public void Clear()
        {
            _itemsDecription.Clear();
        }

        private void SetDecriptionInfo(Sprite icon, string title, string info)
        {
            _descriptionIcon.sprite = icon;
            _descriptionTitle.text = title;
            _descriptionInfo.text = info;
        }
    }
}
