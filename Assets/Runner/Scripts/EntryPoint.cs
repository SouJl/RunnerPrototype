using Runner.Scripts.Enums;
using Runner.Scripts.Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;

namespace Runner.Scripts 
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InputType _inputType;
        [SerializeField] private float _playerSpeed;
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private AnalyticsManager _analytics;
        [SerializeField] private UnityAdsService _adsService;
        [SerializeField] private IAPSevice _iAPSevice;

        private const GameState InitialState = GameState.Start;
        
        private MainContoller _mainContoller;

        void Start()
        {
            var profilePlayer = new ProfilePlayer(_inputType, _playerSpeed, InitialState);
            _mainContoller = new MainContoller(_placeForUi, profilePlayer);

            if (_adsService.IsInitialized) OnAdsInitialized();
            else _adsService.Initialized.AddListener(OnAdsInitialized);
        }

        private void OnDestroy()
        {
            _adsService.Initialized.RemoveListener(OnAdsInitialized);
            _mainContoller.Dispose();
        }

        private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
    }
}

