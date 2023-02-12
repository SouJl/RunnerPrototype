using System;

namespace Runner.Scripts.Interfaces
{
    internal interface IAdsProvider
    {
        event Action OnADSFinished;
        event Action OnADSCanceled;

        void SubscribeADS();
        void UnsubscribeADS();

        void Execute();
    }
}
