using System.Collections.Generic;
using UnityEngine;

namespace Runner.Features.Storage.Upgrade
{
    [CreateAssetMenu(fileName = nameof(UpgradeItemsDataConfig), menuName = "Configs/" + nameof(UpgradeItemsDataConfig))]
    internal class UpgradeItemsDataConfig:ScriptableObject
    {
        [SerializeField] private UpgradeItemConfig[] _itemCongigs;

        public IReadOnlyList<UpgradeItemConfig> ItemConfigs => _itemCongigs;
    }
}
