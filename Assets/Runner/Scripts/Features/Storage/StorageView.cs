using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Storage
{
    internal interface IStorageView
    {
        void Init(UnityAction apply, UnityAction back);
        void Deinit();
    }

    internal class StorageView : MonoBehaviour, IStorageView
    {

        [SerializeField] private Transform _inventoryPlaceUi;

        [SerializeField] private Button _buttonApply;
        [SerializeField] private Button _buttonBack;

        private void OnDestroy() => Deinit();

        public Transform InventoryPlaceUi => _inventoryPlaceUi;

        public void Init(UnityAction apply, UnityAction back)
        {
            _buttonApply.onClick.AddListener(apply);
            _buttonBack.onClick.AddListener(back);
        }

        public void Deinit()
        {
            _buttonApply.onClick.RemoveAllListeners();
            _buttonBack.onClick.RemoveAllListeners();
        }
    }
}
