using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

namespace Services.Analytics
{
    internal interface IAnalyticsManager 
    {
        bool SendMainMenuOpenEnable { get; }
        bool SendGameStartedEnable { get; }

        void SendMainMenuOpen();
        void SendGameStarted(string gameInput);
    }

    internal class AnalyticsManager:MonoBehaviour, IAnalyticsManager
    {
        [Header("Analytics system")]
        [SerializeField] private bool _useUnityAnalytics;
        
        [Space(10)]
        [Header("Using Analytics Events")]
        [SerializeField] private bool _sendMainMenuOpenEnable = true;
        [SerializeField] private bool _sendGameStartedEnabl = true;


        public bool SendMainMenuOpenEnable => _sendMainMenuOpenEnable;
        public bool SendGameStartedEnable => _sendGameStartedEnabl;


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

        public void SendMainMenuOpen() => SendEvent("mainMenuOpened");

        public void SendGameStarted(string gameInput)
        {
            SendEvent("mainGameStarted", new Dictionary<string, object>
            {
                {"gameInputType", gameInput}
            });
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
