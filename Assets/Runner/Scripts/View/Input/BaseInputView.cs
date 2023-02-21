using Runner.Tool;
using UnityEngine;

namespace Runner.View
{
    internal class BaseInputView:MonoBehaviour
    {
        protected float speed;
        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;


        public void Init(
            SubscriptionProperty<float> leftMove, 
            SubscriptionProperty<float> rightMove, 
            float speed)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            this.speed = speed;
        }

        protected void OnLeftMove(float value) => _leftMove.Value = value;
        protected void OnRightMove(float value)=> _rightMove.Value = value;
    }
}
