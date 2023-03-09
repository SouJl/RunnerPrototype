using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runner.Features.Reward
{
    internal interface IRewardView
    {
        int CurrentSlotInActive { get; set; }
        DateTime? TimeGetReward { get; set; }

        void Init(IEnumerable<IRewardItem> rewardItems, 
            RewardPeriodType rewardPeriodType, 
            IEnumerator RewardUpdater, 
            UnityAction claimReward, UnityAction reset, UnityAction exit);
        void Deinit();
        IRewardItem GetActiveReward();
        void UpdateUI(bool isGetReward, string newTimerRewardText);
    }

    internal class RewardView : MonoBehaviour, IRewardView
    {
        [Header("PlayerPrefs Settings")]
        [SerializeField] private string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        [SerializeField] private string TimeGetRewardKey = nameof(TimeGetRewardKey);

        [Header("Ui Elements")]
        [SerializeField] private TMP_Text _timerNewReward;
        [SerializeField] private Transform _mountRootSlotsReward;
        [SerializeField] private GameObject _containerSlotRewardPrefab;
        [SerializeField] private Button _claimRewardButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _exitButton;

        private List<RewardSlotView> _slotViews = new List<RewardSlotView>();
 
        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }

        public void Init(
            IEnumerable<IRewardItem> rewardItems, 
            RewardPeriodType rewardPeriodType,
            IEnumerator RewardUpdater,
            UnityAction claimReward, UnityAction reset, UnityAction exit)
        {
            if (rewardItems == null)
                throw new ArgumentNullException(nameof(rewardItems));

            InitSlots(rewardItems, rewardPeriodType);

            SubscribeButtons(claimReward, reset, exit);

            StartCoroutine(RewardUpdater);
        }

        public void Deinit()
        {
            DeinitSlots();
            UnsubscribeButtons();
            StopAllCoroutines();
        }


        public IRewardItem GetActiveReward() =>
                _slotViews[CurrentSlotInActive].RewardItem;

        public void UpdateUI(bool isGetReward, string newTimerText)
        {
            _claimRewardButton.interactable = isGetReward;
            ChangeTimerText(newTimerText);
            RefreshSlots();
        }

        private void InitSlots(IEnumerable<IRewardItem> rewardItems, RewardPeriodType rewardPeriodType)
        {
            int itemCount = 0;

            foreach (var rewardItem in rewardItems)
            {
                int rewardCooldownTime = itemCount + 1;
                var rewardSlot = CreateSlotRewardView(rewardItem, rewardPeriodType, rewardCooldownTime);                
                _slotViews.Add(rewardSlot);               
                itemCount++;
            }

            RefreshSlots();
        }

        private void DeinitSlots()
        {
            foreach (var slot in _slotViews)
            {
                slot.Deinit();
                Destroy(slot.gameObject);
            }

            _slotViews.Clear();
        }

        private void SubscribeButtons(UnityAction claimReward, UnityAction reset, UnityAction exit)
        {
            _claimRewardButton.onClick.AddListener(claimReward);
            _resetButton.onClick.AddListener(reset);
            _exitButton.onClick.AddListener(exit);
        }

        private void UnsubscribeButtons()
        {
            _claimRewardButton.onClick.RemoveAllListeners();
            _resetButton.onClick.RemoveAllListeners();
        }

        private RewardSlotView CreateSlotRewardView(
            IRewardItem rewardItem, 
            RewardPeriodType rewardPeriodType, 
            int rewardCooldownTime)
        {
            var objectView = Instantiate(_containerSlotRewardPrefab, _mountRootSlotsReward, false);
            RewardSlotView rewardSlotView = objectView.GetComponent<RewardSlotView>();
            rewardSlotView.Init
                (
                    rewardItem,
                    rewardPeriodType,
                    rewardCooldownTime
                );

            return rewardSlotView;
        }

        private void RefreshSlots()
        {
            for (int i = 0; i < _slotViews.Count; i++)
            {
                _slotViews[i].RefreshSlot(i == CurrentSlotInActive);
            }
        }

        private void ChangeTimerText(string newText) =>
            _timerNewReward.text = newText;
    }
}

