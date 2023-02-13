using Runner.Scripts.Interfaces;
using System;

namespace Runner.Scripts.Tool
{
    internal abstract class BaseAdsProvider : IServiceListnerProvider
    {
        public event Action OnTrueResult;
        public event Action OnFalseResult;
 
        public abstract void Subscribe();
        public abstract void Unsubscribe();
   
        public abstract void Execute();

        protected virtual void ADSFinished() => OnTrueResult?.Invoke();

        protected virtual void ADSCanceled() => OnFalseResult?.Invoke();
    }
}
