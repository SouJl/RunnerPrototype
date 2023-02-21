using UnityEngine;
using Runner.Interfaces;

namespace Runner.View
{
    internal class TapeBackgroundView:MonoBehaviour
    {
        [SerializeField] private BackgroundView[] _backgrounds;

        private ISubscriptionProperty<float> _diff;

        public void Init(ISubscriptionProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscriptionOnChange(Move);
        }

        private void OnDestroy()
        {
            _diff?.UnSubscriptionOnChange(Move);
        }

        private void Move(float value)
        {
            foreach(var background in _backgrounds) 
            {
                background.Move(-value);
            }
        }
    }
}
