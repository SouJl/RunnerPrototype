using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    internal class DailyRewardController
    {
        private readonly DailyRewardView _rewardView;
        private readonly ICurrencyView _currencyView;

        private List<ContainerSlotRewardView> _slots;
        private Coroutine _coroutine;

        private bool _isGetReward;
        private bool _isInitialized;

        public DailyRewardController(DailyRewardView rewardView, ICurrencyView currencyView) 
        {
            _rewardView 
                = rewardView ?? throw new ArgumentNullException(nameof(rewardView));
            _currencyView
               = currencyView ?? throw new ArgumentNullException(nameof(currencyView));
        }
            

        public void Init()
        {
            if (_isInitialized)
                return;

            InitSlots();
            RefreshUi();
            StartRewardsUpdating();
            SubscribeButtons();

            _isInitialized = true;
        }


        public void Deinit()
        {
            if (!_isInitialized)
                return;

            DeinitSlots();
            StopRewardsUpdating();
            UnsubscribeButtons();

            _isInitialized = false;
        }

        private void InitSlots()
        {
            _slots = new List<ContainerSlotRewardView>();

            for (int i = 0; i < _rewardView.Rewards.Count; i++)
            {
                ContainerSlotRewardView instanceSlot = CreateSlotRewardView();
                _slots.Add(instanceSlot);
            }
        }

        private ContainerSlotRewardView CreateSlotRewardView() =>
            Object.Instantiate
            (
                _rewardView.ContainerSlotRewardPrefab,
                _rewardView.MountRootSlotsReward,
                false
            );

        private void DeinitSlots()
        {
            foreach (ContainerSlotRewardView slot in _slots)
                Object.Destroy(slot.gameObject);

            _slots.Clear();
        }


        private void StartRewardsUpdating() =>
            _coroutine = _rewardView.StartCoroutine(RewardsStateUpdater());

        private void StopRewardsUpdating()
        {
            if (_coroutine == null)
                return;

            _rewardView.StopCoroutine(_coroutine);
            _coroutine = null;
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


        private void SubscribeButtons()
        {
            _rewardView.ClaimRewardButton.onClick.AddListener(ClaimReward);
            _rewardView.ResetButton.onClick.AddListener(ResetRewardsState);
        }

        private void UnsubscribeButtons()
        {
            _rewardView.ClaimRewardButton.onClick.RemoveListener(ClaimReward);
            _rewardView.ResetButton.onClick.RemoveListener(ResetRewardsState);
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            Reward reward = _rewardView.Rewards[_rewardView.CurrentSlotInActive];

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
                timeFromLastRewardGetting.Seconds >= _rewardView.TimeDeadline;

            bool isTimeToGetNewReward =
                timeFromLastRewardGetting.Seconds >= _rewardView.TimeCooldown;

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
            _rewardView.ClaimRewardButton.interactable = _isGetReward;
            _rewardView.TimerNewReward.text = GetTimerNewRewardText();
            RefreshSlots();
        }

        private string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "The reward is ready to be received!";

            if (_rewardView.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _rewardView.TimeGetReward.Value.AddSeconds(_rewardView.TimeCooldown);
                TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

                string timeGetReward =
                    $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                    $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                return $"Time to get the next reward: {timeGetReward}";
            }

            return string.Empty;
        }

        private void RefreshSlots()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                Reward reward = _rewardView.Rewards[i];
                int countDay = i + 1;
                bool isSelected = i == _rewardView.CurrentSlotInActive;

                _slots[i].SetData(reward, countDay, isSelected);
            }
        }
    }
}
