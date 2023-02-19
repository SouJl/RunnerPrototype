using UnityEngine;
using System.Collections.Generic;

namespace Features.AbilitySystem.Abilities
{
    [CreateAssetMenu(
       fileName = nameof(AbilityItemsDataConfig),
       menuName = "Configs/" + nameof(AbilityItemsDataConfig))]
    internal class AbilityItemsDataConfig:ScriptableObject
    {
        [SerializeField] private AbilityItemConfig[] _abilityConfigs;

        public IReadOnlyList<AbilityItemConfig> AbilityConfigs => _abilityConfigs;
    }
}
