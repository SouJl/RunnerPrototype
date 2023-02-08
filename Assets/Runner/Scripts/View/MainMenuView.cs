using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runner.Scripts.View
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        public void Init(UnityAction startGame, UnityAction openSetting, UnityAction exit)
        {
            _startButton.onClick.AddListener(startGame);
            _settingsButton.onClick.AddListener(openSetting);
            _exitButton.onClick.AddListener(exit);
        }

        protected void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}
