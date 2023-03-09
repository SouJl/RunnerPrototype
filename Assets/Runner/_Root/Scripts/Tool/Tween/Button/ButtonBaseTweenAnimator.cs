using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Tool.Tweens
{
    internal abstract class ButtonBaseTweenAnimator : MonoBehaviour, ITweenAnimator
    {
        [Header("Components")]
        [SerializeField] protected Button _button;
        [SerializeField] protected RectTransform _rectTransform;

        [Header("Base Settings")]
        [SerializeField] protected AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] protected bool _isIndependentUpdate = true;
        [SerializeField] protected Ease _curveEase = Ease.Linear;

        protected Tweener activeTween;

        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();
        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void InitComponents()
        {
            _button ??= GetComponent<Button>();
            _rectTransform ??= GetComponent<RectTransform>();

        }

        private void OnButtonClick() => PlayAnimation();

        public abstract void PlayAnimation();
        public abstract void StopAnimation();
    }
}
