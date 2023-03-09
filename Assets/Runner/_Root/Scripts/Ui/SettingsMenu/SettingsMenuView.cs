using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runner.UI
{
    internal class SettingsMenuView : MonoBehaviour
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
