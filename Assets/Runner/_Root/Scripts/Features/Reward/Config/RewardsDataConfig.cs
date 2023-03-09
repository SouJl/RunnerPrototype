using UnityEngine;
using System.Collections.Generic;

namespace Runner.Features.Reward
{
    internal interface IRewardDataConfig
    {
        RewardPeriodType RewardPeriodType { get; }
        float TimeCooldown { get; }
        float TimeDeadline { get; }
        IReadOnlyList<IRewardItem> RewardItemConfigs { get; }
    }

    [CreateAssetMenu(
     fileName = nameof(RewardsDataConfig),
     menuName = "Configs/Reward/" + nameof(RewardsDataConfig))]
    internal class RewardsDataConfig :ScriptableObject, IRewardDataConfig
    {
        [field: SerializeField] public RewardPeriodType RewardPeriodType { get; private set; }
        [field: SerializeField] public float TimeCooldown { get; private set; } = 86400;
        [field: SerializeField] public float TimeDeadline { get; private set; } = 172800;

        [SerializeField] private RewardItemConfig[] _rewardItemConfigs;
        
        public IReadOnlyList<IRewardItem> RewardItemConfigs => _rewardItemConfigs;
    }
}
