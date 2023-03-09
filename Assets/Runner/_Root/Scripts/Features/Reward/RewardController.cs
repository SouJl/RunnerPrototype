using Runner.Profile;
using Runner.Tool;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.Features.Reward
{
    internal class RewardController : BaseController
    {
        private readonly ResourcePath _configPath = new ResourcePath("Configs/Rewards/DailyRewardsDataConfig");
        
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Rewards/DailyRewardView");

        private readonly IRewardDataConfig _config;
        private readonly IRewardView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly CurrencyController _currencyController;

    //    private readonly ICurrencyView _currencyView;
 
        private bool _isGetReward;

        public RewardController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _config = LoadConfig(_configPath);
            _view = LoadView(placeForUi);
            
            _profilePlayer 
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _currencyController = CreateCurrencyController(placeForUi, _profilePlayer.Currency);

            InitView();
        }


        private IRewardDataConfig LoadConfig(ResourcePath configPath) => 
            ResourceLoader.LoadObject<RewardsDataConfig>(configPath);

        private IRewardView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourceLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<RewardView>();
        }

        private CurrencyController CreateCurrencyController(Transform placeForUi, ICurrencyModel currencyModel)
        {
            var currencyController = new CurrencyController(placeForUi, currencyModel);
            AddController(currencyController);

            return currencyController;
        }

        protected override void OnDispose()
        {
            _view.Deinit();
            base.OnDispose();
        }

        public void InitView()
        {
            _view.Init
                (
                    _config.RewardItemConfigs, 
                    _config.RewardPeriodType, 
                    RewardsStateUpdater(), 
                    ClaimReward, 
                    ResetRewardsState
                );

            RefreshUi();
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

            IRewardItem reward = _view.GetActiveReward();
            
            switch (reward.RewardType)
            {
                case RewardType.Scheme:
                    _profilePlayer.Currency.Scheme += reward.CountCurrency;
                    break;
                case RewardType.Diamond:
                    _profilePlayer.Currency.Diamond += reward.CountCurrency;
                    break;
            }

            _view.TimeGetReward = DateTime.UtcNow;
            _view.CurrentSlotInActive++;
            RefreshRewardsState();
        }


        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _view.TimeGetReward.HasValue;
            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting =
                DateTime.UtcNow - _view.TimeGetReward.Value;

            bool isDeadlineElapsed =
                timeFromLastRewardGetting.Seconds >= _config.TimeDeadline;

            bool isTimeToGetNewReward =
                timeFromLastRewardGetting.Seconds >= _config.TimeCooldown;

            if (isDeadlineElapsed)
                ResetRewardsState();

            _isGetReward = isTimeToGetNewReward;

        }

        private void ResetRewardsState()
        {
            _view.TimeGetReward = null;
            _view.CurrentSlotInActive = 0;
        }


        private void RefreshUi()
        {
            string newTimerRewardText = GetTimerNewRewardText();
            _view.UpdateUI(_isGetReward, newTimerRewardText);
        }

        private string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "The reward is ready to be received!";

            if (_view.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _view.TimeGetReward.Value.AddSeconds(_config.TimeCooldown);
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
