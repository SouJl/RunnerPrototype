using UnityEngine;

namespace Rewards
{
    internal class MainView : MonoBehaviour
    {
        [SerializeField] private DailyRewardView _dailyRewardView;
        [SerializeField] private CurrencyView _currencyView;

        private DailyRewardController _dailyRewardController;

        private void Awake() =>
            _dailyRewardController = new DailyRewardController(_dailyRewardView, _currencyView);

        private void Start() =>
            _dailyRewardController.Init();

        private void OnDestroy() =>
            _dailyRewardController.Deinit();

    }
}
