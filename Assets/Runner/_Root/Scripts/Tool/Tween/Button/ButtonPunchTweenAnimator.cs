using DG.Tweening;
using UnityEngine;

namespace Runner.Tool.Tweens
{
    internal class ButtonPunchTweenAnimator : ButtonBaseTweenAnimator
    {
        [Header("Punch Settings")]
        [SerializeField] private Vector3 _punch = Vector3.forward;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private int _vibrato = 10;


        [ContextMenu(nameof(PlayAnimation))]
        public override void PlayAnimation()
        {
            StopAnimation();

            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    activeTween = _rectTransform.DOPunchRotation(_punch, _duration, _vibrato)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;

                case AnimationButtonType.ChangePosition:
                    activeTween = _rectTransform.DOPunchPosition(_punch, _duration, _vibrato)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;
                case AnimationButtonType.ChangeScale:
                    activeTween = _rectTransform.DOPunchScale(_punch, _duration, _vibrato)
                        .SetEase(_curveEase)
                        .SetUpdate(_isIndependentUpdate);
                    break;
            }
        }

        [ContextMenu(nameof(StopAnimation))]
        public override void StopAnimation() =>
            activeTween?.Kill();
    }
}
