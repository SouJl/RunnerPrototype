using System;
using UnityEngine;

namespace Runner.Features.Reward
{
    [Serializable]
    internal class Reward
    {
        [field: SerializeField] public RewardType RewardType { get; private set; }
        [field: SerializeField] public Sprite IconCurrency { get; private set; }
        [field: SerializeField] public int CountCurrency { get; private set; }
    }
}
