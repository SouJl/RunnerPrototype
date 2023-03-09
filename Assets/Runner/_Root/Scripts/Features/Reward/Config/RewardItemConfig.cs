using UnityEngine;

namespace Runner.Features.Reward
{
    internal interface IRewardItem
    {
        RewardType RewardType { get; }
        Sprite IconCurrency { get; }
        int CountCurrency { get; }
    }

    [CreateAssetMenu(fileName = nameof(RewardItemConfig),
        menuName = "Configs/Reward/" + nameof(RewardItemConfig))]
    internal class RewardItemConfig : ScriptableObject, IRewardItem
    {
        [field: SerializeField] public RewardType RewardType { get; private set; }
        [field: SerializeField] public Sprite IconCurrency { get; private set; }
        [field: SerializeField] public int CountCurrency { get; private set; }
    }
}
