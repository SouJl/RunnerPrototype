using Runner.Scripts.Interfaces;
using System;

namespace Runner.Scripts.Tool
{
    internal abstract class BaseAdsProvider : IAdsProvider
    {
        public event Action OnADSFinished;
        public event Action OnADSCanceled;

        public abstract void Execute();

        public abstract void SubscribeADS();

        public abstract void UnsubscribeADS();


        protected virtual void ADSFinished() => OnADSFinished?.Invoke();

        protected virtual void ADSCanceled() => OnADSCanceled?.Invoke();
    }
}
