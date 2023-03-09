using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runner.UI
{
    internal interface IGameMenuView
    {
        void Init(UnityAction mainMenuBack, UnityAction openPauseMenu);
    }

    internal class GameMenuView :MonoBehaviour, IGameMenuView
    {
        [SerializeField] private Button _mainMenuBackButton;
        [SerializeField] private Button _openPauseMenuButton;

        public void Init(UnityAction mainMenuBack, UnityAction openPauseMenu)
        {
            _mainMenuBackButton.onClick.AddListener(mainMenuBack);
            _openPauseMenuButton.onClick.AddListener(openPauseMenu);
        }
        private void OnDestroy()
        {
            _mainMenuBackButton.onClick.RemoveAllListeners();
            _openPauseMenuButton.onClick.RemoveAllListeners();
        }
    }
}
