using System;
using UnityEngine;

namespace Runner.UI
{
    internal interface IPauseMenuModel
    {
        event Action Enabled;
        event Action Disabled;
        public bool IsEnabled { get; }

        void Enable();
        void Disable();
    }

    internal class PauseMenuModel : IPauseMenuModel
    {
        private const float _pauseTimeScale = 0f;
        private readonly float _initialTimeScale;
        private bool _isDisposed;

        public event Action Enabled;
        public event Action Disabled;
        public bool IsEnabled { get; private set; }

        public PauseMenuModel()
        {
            _initialTimeScale = Time.timeScale;
        }

        public void Enable()
        {
            if (IsEnabled)
                throw new InvalidOperationException("Pause already enabled!");

            Time.timeScale = _pauseTimeScale;
            IsEnabled = true;
            Enabled?.Invoke();
        }

        public void Disable()
        {
            if (!IsEnabled)
                throw new InvalidOperationException("Pause already disabled!");

            Time.timeScale = _initialTimeScale;
            IsEnabled = false;
            Disabled?.Invoke();
        }
     
    }
}
