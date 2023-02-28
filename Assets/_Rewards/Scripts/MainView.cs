using UnityEngine;

namespace Rewards
{
    internal class MainView : MonoBehaviour
    {
        [SerializeField] private RewardView _dailyRewardView;
        [SerializeField] private CurrencyView _currencyView;

        private RewardController _dailyRewardController;

        private void Awake() =>
            _dailyRewardController = new RewardController(_dailyRewardView, _currencyView);

        private void Start() =>
            _dailyRewardController.Init();

        private void OnDestroy() =>
            _dailyRewardController.Deinit();

    }
}
