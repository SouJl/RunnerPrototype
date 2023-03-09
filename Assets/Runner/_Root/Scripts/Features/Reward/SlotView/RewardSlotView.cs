using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Features.Reward
{
    internal class RewardSlotView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textDays;
        [SerializeField] private TMP_Text _countReward;

        public IRewardItem RewardItem { get; private set; }

        public void Init(IRewardItem rewardItem, RewardPeriodType rewardPeriod, int countDay)
        {
            RewardItem 
                = rewardItem ?? throw new ArgumentNullException(nameof(rewardItem));

            _iconCurrency.sprite = RewardItem.IconCurrency;
            _countReward.text = RewardItem.CountCurrency.ToString();
            _textDays.text = $"{rewardPeriod} {countDay}";
        }

        public void Deinit()
        {
            _iconCurrency.sprite = default;
            _countReward.text = default;
            _textDays.text = default;
        }

        public void RefreshSlot(bool isSelect)
        {
            UpdateBackground(isSelect);
        }

        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}
