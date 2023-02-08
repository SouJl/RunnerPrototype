using JoostenProductions;
using UnityEngine;

namespace Runner.Scripts.View
{
    internal class InputAccelerationView:BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 0.05f;

        private const float normolizedMagnitude = 1f;

        private void Start()
        {
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

        private void Move() 
        {
            Vector3 direction = CalcDirection();
            float moveValue = speed * _inputMultiplier * Time.deltaTime * direction.x;
            float absValue = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);
            if(sign > 0) 
            {
                OnRightMove(absValue);
            }
            else 
            {
                OnLeftMove(absValue);
            }
        }

        private Vector3 CalcDirection()
        {
            Vector3 direction = Vector3.zero;
            direction.x = -Input.acceleration.y;
            direction.z = Input.acceleration.x;
            if(direction.sqrMagnitude > normolizedMagnitude) 
            {
                direction.Normalize();
            }
            return direction;
        }
    }
}
