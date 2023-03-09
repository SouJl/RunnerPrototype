using DG.Tweening;
using UnityEngine;

namespace Runner.Tool.Tweens
{
    internal class ButtonShakeTweenAnimator : ButtonBaseTweenAnimator
    {
        [Header("Punch Settings")]
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private int _vibrato = 10;

        [ContextMenu(nameof(PlayAnimation))]
        public override void PlayAnimation()
        {
            StopAnimation();

            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    activeTween = _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength, _vibrato)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;

                case AnimationButtonType.ChangePosition:
                    activeTween = _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength, _vibrato)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;
                case AnimationButtonType.ChangeScale:
                    activeTween = _rectTransform.DOPunchScale(Vector2.one * _strength, _duration, _vibrato)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;
            }
        }

        [ContextMenu(nameof(StopAnimation))]
        public override void StopAnimation()
        {
            activeTween?.Kill();
            activeTween = default;
        }


    }
}
