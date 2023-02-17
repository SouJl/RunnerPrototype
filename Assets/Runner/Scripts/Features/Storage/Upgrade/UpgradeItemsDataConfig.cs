using System.Collections.Generic;
using UnityEngine;

namespace Features.Storage.Upgrade
{
    [CreateAssetMenu(fileName = nameof(UpgradeItemConfig), menuName = "Configs/" + nameof(UpgradeItemConfig))]
    internal class UpgradeItemsDataConfig:ScriptableObject
    {
        [SerializeField] private UpgradeItemConfig[] _itemCongigs;

        public IReadOnlyList<UpgradeItemConfig> ItemConfigs => _itemCongigs;
    }
}
