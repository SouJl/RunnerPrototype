using Runner.Profile;
using Runner.Services;
using Runner.Services.ADS;
using UnityEngine;

namespace Runner 
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InitialProfileData _initialData;
        [SerializeField] private Transform _placeForUi;
        
        private MainContoller _mainContoller;

        private BaseAdsProvider _interstitialProvider;

        void Start()
        {
            var profilePlayer = new ProfilePlayer(_initialData);
            _mainContoller = new MainContoller(_placeForUi, profilePlayer);

            _interstitialProvider = new InterstitialAdsProvider();
            _interstitialProvider.Subscribe();

            if (ServicesHandler.AdsService.IsInitialized) OnAdsInitialized();
            else ServicesHandler.AdsService.Initialized.AddListener(OnAdsInitialized);

            ServicesHandler.PushNotification.CreateNotification();
        }

        private void OnDestroy()
        {
            ServicesHandler.AdsService.Initialized.RemoveListener(OnAdsInitialized);
            _interstitialProvider.Unsubscribe();
            _mainContoller.Dispose();
        }

        private void OnAdsInitialized() => _interstitialProvider.Execute();
    }
}

