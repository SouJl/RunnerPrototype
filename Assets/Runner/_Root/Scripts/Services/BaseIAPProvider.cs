﻿using Runner.Interfaces;
using System;

namespace Runner.Services
{
    internal abstract class BaseIAPProvider : IServiceListnerProvider
    {
        public event Action OnTrueResult;
        public event Action OnFalseResult;

        public abstract void Subscribe();
        public abstract void Unsubscribe();

        public abstract void Execute(string productId);

        protected virtual void OnIAPSucceed() => OnTrueResult?.Invoke();

        protected virtual void OnIAPFailed() => OnFalseResult?.Invoke();
    }
}
