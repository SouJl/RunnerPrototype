using Runner.Tool.Notification.Push;
using Services.Ads;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;

namespace Runner.Services
{
    internal class ServicesHandler:MonoBehaviour
    {
        [SerializeField] private AnalyticsManager _analyticsManager;
        [SerializeField] private UnityAdsService _adsService;
        [SerializeField] private IAPSevice _iAPSevice;
        [SerializeField] private PushNotificationProvider _pushNotification;

        private static ServicesHandler _instance;
        private static ServicesHandler Instance => _instance ??= FindObjectOfType<ServicesHandler>();

        public static IAnalyticsManager Analytics => Instance._analyticsManager;
        public static IAdsService AdsService => Instance._adsService;
        public static IIAPService IAPService => Instance._iAPSevice;
        public static IPushNotification PushNotification => Instance._pushNotification;

        private void Awake()
        {
            _instance ??= this;
        }
    }
}
