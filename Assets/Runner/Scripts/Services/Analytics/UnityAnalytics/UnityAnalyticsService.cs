using System.Collections.Generic;
using Unity.Services.Analytics;

namespace Services.Analytics
{
    internal class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName, Dictionary<string, object> eventData)
            => AnalyticsService.Instance.CustomData(eventName, eventData);

        public void SendEvent(string eventName)
        {
            
        }
    }
}
