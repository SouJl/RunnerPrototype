using Services.Ads.UnityAds.Settings;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace Services.Ads.UnityAds
{
    internal class UnityAdsService : MonoBehaviour, IAdsService, IUnityAdsInitializationListener
    {
        [Header("Components")]
        [SerializeField] private UnityAdsSettings _settings;

        [Header("Unity Ads Events")]
        [SerializeField] private UnityEvent _initialized;

        public UnityEvent Initialized => _initialized;

        public IAdsPlayer InterstitialPlayer { get; private set; }
        public IAdsPlayer RewardedPlayer { get; private set; }
        public IAdsPlayer BannerPlayer { get; private set; }

        public bool IsInitialized => Advertisement.isInitialized;

        private void Awake()
        {
            InitializeAds();
            InitializePlayers();
        }


        public void InitializeAds()
        {
            Advertisement.Initialize(
                _settings.GameId,
                _settings.TestMode,
                _settings.EneblePerPlacementMode,
                this
                );
        }

        private void InitializePlayers()
        {
            InterstitialPlayer = CreateInterstitial();
            RewardedPlayer = CreateRewarded();
            BannerPlayer = CreateBanner();
        }

        private IAdsPlayer CreateInterstitial()
        {
            return _settings.Interstitial.Enabled
                ? new InterstitialPlayer(_settings.Interstitial.Id)
                : new StubPlayer("");
        }

        private IAdsPlayer CreateRewarded()
        {
            return _settings.Rewarded.Enabled 
                ? new RewardedPlayer(_settings.Rewarded.Id) 
                : new StubPlayer("");
        }

        private IAdsPlayer CreateBanner() => new StubPlayer("");


        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            Log("Unity Ads initialization complete.");
            _initialized?.Invoke();
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Error($"Unity Ads Initialization Failed: {error} - {message}");
        }


        private void Log(string message) => Debug.Log(WrapMessage(message));

        private void Error(string message) => Debug.LogError(WrapMessage(message));

        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
