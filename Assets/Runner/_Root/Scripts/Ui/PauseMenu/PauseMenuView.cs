using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runner.UI
{
    internal interface IPauseMenuView
    {
        void Init(UnityAction resume, UnityAction mainMenu);
        void Show();
        void Hide();

    }
    internal class PauseMenuView : MonoBehaviour, IPauseMenuView
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;

        public void Init(UnityAction resume, UnityAction mainMenu)
        {
            _resumeButton.onClick.AddListener(resume);
            _mainMenuButton.onClick.AddListener(mainMenu);
        }

        private void OnDestroy()
        {
            _resumeButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.RemoveAllListeners();
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
