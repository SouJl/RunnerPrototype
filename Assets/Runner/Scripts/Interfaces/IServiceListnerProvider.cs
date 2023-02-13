using System;

namespace Runner.Scripts.Interfaces
{
    internal interface IServiceListnerProvider
    {
        event Action OnTrueResult;
        event Action OnFalseResult;

        void Subscribe();
        void Unsubscribe();
    }
}
