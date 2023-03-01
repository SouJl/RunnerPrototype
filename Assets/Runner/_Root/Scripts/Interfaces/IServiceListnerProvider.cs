using System;

namespace Runner.Interfaces
{
    internal interface IServiceListnerProvider
    {
        event Action OnTrueResult;
        event Action OnFalseResult;

        void Subscribe();
        void Unsubscribe();
    }
}
