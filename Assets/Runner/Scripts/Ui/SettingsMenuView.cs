using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runner.Scripts.Ui
{
    internal class SettingsMenuView
    {
        [SerializeField] private Button _backToMenuButton;

        public void Init(UnityAction back)
        {
            _backToMenuButton.onClick.AddListener(back);
        }

        protected void OnDestroy()
        {
            _backToMenuButton.onClick.RemoveAllListeners();
        }

    }
}
