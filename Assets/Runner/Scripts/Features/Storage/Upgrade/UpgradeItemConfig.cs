using Features.Inventory.Items;
using UnityEngine;

namespace Features.Storage.Upgrade
{
    [CreateAssetMenu(fileName = nameof(UpgradeItemConfig), menuName = "Configs/" + nameof(UpgradeItemConfig))]
    internal class UpgradeItemConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig _itemConfig;
        [field: SerializeField] public UpgradeType Type { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public string Id => _itemConfig.Id;
    }
}
