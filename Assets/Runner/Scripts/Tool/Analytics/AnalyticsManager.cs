using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

namespace Runner.Scripts.Tool.Analytics
{
    internal class AnalyticsManager:MonoBehaviour
    {
        [SerializeField] private bool _useUnityAnalytics;

        private List<IAnalyticsService> _services;

        private void Awake()
        {
            _services = new List<IAnalyticsService>();
            
            if (_useUnityAnalytics) 
            {
                _services.Add(new UnityAnalyticsService());
            }
        }

        async void Start()
        {
            try
            {
                if (_useUnityAnalytics)
                {
                    await UnityServices.InitializeAsync();
                }

            }
            catch (ConsentCheckException e)
            {
                Debug.Log(e.ToString());
            }
        }

        private void SendEvent(string eventName) 
        {
            foreach (var service in _services)
                service.SendEvent(eventName);
        }

        private void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            foreach (var service in _services)
                service.SendEvent(eventName, eventData);
        }
    }
}
