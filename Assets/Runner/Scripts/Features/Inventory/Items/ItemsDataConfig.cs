using UnityEngine;
using System.Collections.Generic;

namespace Features.Inventory.Items
{
    [CreateAssetMenu(fileName = nameof(ItemsDataConfig), menuName = "Configs/" + nameof(ItemsDataConfig))]
    internal sealed class ItemsDataConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _itemConfigs;

        public IReadOnlyList<ItemConfig> ItemConfigs => _itemConfigs;
    }
}
