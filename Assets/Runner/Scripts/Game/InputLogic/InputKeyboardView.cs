using JoostenProductions;
using UnityEngine;

namespace Runner.Game
{
    internal class InputKeyboardView:BaseInputView
    {
        [Header("Keyboard Input Settings")]
        [SerializeField] private float _inputMultiplier = 0.01f;

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
            float moveValue = speed * _inputMultiplier * Time.deltaTime;

            if (Input.GetKey(KeyCode.RightArrow))
                OnRightMove(moveValue);
            if (Input.GetKey(KeyCode.LeftArrow))
                OnLeftMove(moveValue);
        }
    }
}
