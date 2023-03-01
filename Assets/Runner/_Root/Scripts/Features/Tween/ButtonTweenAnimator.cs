using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Features.Tweens
{
    public class ButtonTweenAnimator : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private bool _isIndependentUpdate = true;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;

        private Tweener _activeTween;
        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();


        private void InitComponents()
        {
            _button ??= GetComponent<Button>();
            _rectTransform ??= GetComponent<RectTransform>();
            
        }

        private void OnButtonClick() =>
            ActivateAnimation();

        [ContextMenu(nameof(ActivateAnimation))]
        private void ActivateAnimation()
        {
           
            StopAnimation();

            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _activeTween = _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;

                case AnimationButtonType.ChangePosition:
                    _activeTween = _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;
            }
        }

        [ContextMenu(nameof(StopAnimation))]
        private void StopAnimation() =>
            _activeTween?.Kill();

    }
}
