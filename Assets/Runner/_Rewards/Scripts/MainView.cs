using System;
using System.Linq;
using UnityEngine;

namespace Rewards
{
    internal class MainView : MonoBehaviour
    {

        [SerializeField] private RewardsDataConfig _rewardsDataConfig;
        [SerializeField] private RewardView _dailyRewardView;
        [SerializeField] private CurrencyView _currencyView;

        private RewardController _dailyRewardController;

        private void Awake()
        { 
            _dailyRewardController = new RewardController(_dailyRewardView, _currencyView, _rewardsDataConfig);
        }

        private void OnDestroy() =>
            _dailyRewardController.Dispose();

    }
}
