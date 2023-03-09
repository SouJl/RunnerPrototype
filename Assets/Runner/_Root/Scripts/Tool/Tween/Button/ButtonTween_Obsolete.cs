using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runner.Tool.Tweens
{
    [System.Obsolete]
    [RequireComponent(typeof(RectTransform))]
    public class ButtonTween_Obsolete : Button
    {

        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private int _vibrato = 10;
        [SerializeField] protected bool _isIndependentUpdate = true;

        public static string AnimationTypeName => nameof(_animationButtonType);
        public static string CurveEaseName => nameof(_curveEase);
        public static string DurationName => nameof(_duration);

        public static string Vibrato => nameof(_vibrato);
        public static string IsIndependentUpdate => nameof(_isIndependentUpdate);

        protected override void Awake()
        {
            base.Awake();
            InitRectTransform();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            InitRectTransform();
        }

        private void InitRectTransform() =>
            _rectTransform ??= GetComponent<RectTransform>();


        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ActivateAnimation();
        }

        private void ActivateAnimation()
        {
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength, _vibrato)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;

                case AnimationButtonType.ChangePosition:
                    _rectTransform.DOShakePosition(_duration, Vector2.one * _strength, _vibrato)
                       .SetEase(_curveEase)
                       .SetUpdate(_isIndependentUpdate);
                    break;
                case AnimationButtonType.ChangeScale:
                    _rectTransform.DOPunchScale(Vector2.one * _strength, _duration, _vibrato)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;
            }
        }
    }
}
