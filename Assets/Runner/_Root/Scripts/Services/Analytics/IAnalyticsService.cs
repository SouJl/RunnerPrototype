﻿using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Services.Analytics
{
    internal interface IAnalyticsService
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, Dictionary<string, object> eventData);
        void SendTransaction(string productId, long amount, string currency, IEnumerable<PayoutDefinition> payots);
    }
}
