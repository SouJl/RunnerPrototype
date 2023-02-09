using System.Collections.Generic;

namespace Runner.Scripts.Tool.Analytics
{
    internal interface IAnalyticsService
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, Dictionary<string, object> eventData);
    }
}
