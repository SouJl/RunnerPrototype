using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runner.View
{
    internal class MainMenuView : MonoBehaviour
    {
        [Header("Additional Settings")]
        [SerializeField] private string _buyProductId;

        [Header("MainMenu Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _storageButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _playRewardedButton;
        [SerializeField] private Button _buyProductButton;

        public void Init(UnityAction startGame, UnityAction openSetting, 
            UnityAction storageButton, UnityAction exit, 
            UnityAction playRewarded, UnityAction<string> buyProduct)
        {
            _startButton.onClick.AddListener(startGame);
            _settingsButton.onClick.AddListener(openSetting);
            _storageButton.onClick.AddListener(storageButton);
            _exitButton.onClick.AddListener(exit);
            _playRewardedButton.onClick.AddListener(playRewarded);
            _buyProductButton.onClick.AddListener(() => buyProduct(_buyProductId));
        }

        protected void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _storageButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _playRewardedButton.onClick.RemoveAllListeners();
            _buyProductButton.onClick.RemoveAllListeners();
        }
    }
}
