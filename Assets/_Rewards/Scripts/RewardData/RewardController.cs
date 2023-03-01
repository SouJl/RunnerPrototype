using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Rewards
{
    internal class RewardController : IDisposable
    {
        private readonly IRewardView _rewardView;
        private readonly ICurrencyView _currencyView;
        private readonly IRewardDataConfig _rewardConfig;

        private bool _isGetReward;


        public RewardController(
            [NotNull] IRewardView rewardView,
            [NotNull] ICurrencyView currencyView,
            [NotNull] IRewardDataConfig rewardConfig) 
        {
            _rewardView 
                = rewardView ?? throw new ArgumentNullException(nameof(rewardView));
            _currencyView
               = currencyView ?? throw new ArgumentNullException(nameof(currencyView));
            _rewardConfig
                = rewardConfig ?? throw new ArgumentNullException(nameof(rewardConfig));
            
            InitView();
        }

        public void InitView()
        {
            _rewardView.Init
                (
                    _rewardConfig.RewardItemConfigs, 
                    _rewardConfig.RewardPeriodType, 
                    RewardsStateUpdater(), 
                    ClaimReward, 
                    ResetRewardsState
                );

            RefreshUi();
        }

        public void Dispose()
        {
            _rewardView.Deinit();
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1);

            while (true)
            {
                RefreshRewardsState();
                RefreshUi();
                yield return waitForSecond;
            }
        }


        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            IRewardItem reward = _rewardView.GetActiveReward();
            
            switch (reward.RewardType)
            {
                case RewardType.Scheme:
                    _currencyView.AddScheme(reward.CountCurrency);
                    break;
                case RewardType.Diamond:
                    _currencyView.AddDiamond(reward.CountCurrency);
                    break;
            }

            _rewardView.TimeGetReward = DateTime.UtcNow;
            _rewardView.CurrentSlotInActive++;
            RefreshRewardsState();
        }


        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _rewardView.TimeGetReward.HasValue;
            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting =
                DateTime.UtcNow - _rewardView.TimeGetReward.Value;

            bool isDeadlineElapsed =
                timeFromLastRewardGetting.Seconds >= _rewardConfig.TimeDeadline;

            bool isTimeToGetNewReward =
                timeFromLastRewardGetting.Seconds >= _rewardConfig.TimeCooldown;

            if (isDeadlineElapsed)
                ResetRewardsState();

            _isGetReward = isTimeToGetNewReward;

        }

        private void ResetRewardsState()
        {
            _rewardView.TimeGetReward = null;
            _rewardView.CurrentSlotInActive = 0;
        }


        private void RefreshUi()
        {
            string newTimerRewardText = GetTimerNewRewardText();
            _rewardView.UpdateUI(_isGetReward, newTimerRewardText);
        }

        private string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "The reward is ready to be received!";

            if (_rewardView.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _rewardView.TimeGetReward.Value.AddSeconds(_rewardConfig.TimeCooldown);
                TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

                string timeGetReward =
                    $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                    $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                return $"Time to get the next reward: {timeGetReward}";
            }

            return string.Empty;
        }
    }
}
