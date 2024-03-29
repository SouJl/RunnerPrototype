﻿using System;

namespace Runner.Interfaces
{
    internal interface ISubscriptionProperty<T>
    {
        T Value { get; }
        void SubscriptionOnChange(Action<T> subscriptionAction);
        void UnSubscriptionOnChange(Action<T> unsubscriptionAction);
    }
}
