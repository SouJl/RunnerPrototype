using JoostenProductions;
using UnityEngine;

namespace Runner.Scripts.View
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

            if (Input.GetKeyDown(KeyCode.RightArrow))
                OnRightMove(moveValue);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                OnLeftMove(moveValue);
        }
    }
}
