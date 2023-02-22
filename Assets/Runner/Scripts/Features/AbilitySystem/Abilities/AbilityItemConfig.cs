using Runner.Features.Inventory.Items;
using UnityEngine;

namespace Runner.Features.AbilitySystem.Abilities
{

    internal interface IAbilityItem
    {
        string Id { get; }
        Sprite Icon { get; }
        AbilityType Type { get; }
        GameObject ExecuteObject { get; }
        float Value { get; }
    }


    [CreateAssetMenu(fileName = nameof(AbilityItemConfig), 
        menuName = "Configs/" + nameof(AbilityItemConfig))]
    internal class AbilityItemConfig:ScriptableObject, IAbilityItem
    {
        [SerializeField] private ItemConfig _itemConfig;
        [field: SerializeField] public AbilityType Type { get; private set; }
        [field: SerializeField] public GameObject ExecuteObject { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public string Id => _itemConfig.Id;
        public Sprite Icon => _itemConfig.Icon;
    }
}