using UnityEngine;
using UnityEngine.UI;

namespace Runner.Tool.Audio
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(AudioSource))]
    internal class AudioButtonComponent : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] protected Button _button;
        [SerializeField] protected AudioSource _audioSource;

        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void InitComponents()
        {
            _button ??= GetComponent<Button>();
            _audioSource ??= GetComponent<AudioSource>();

            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick() => ActivateSound();
        private void ActivateSound() => _audioSource.Play();

    }
}
