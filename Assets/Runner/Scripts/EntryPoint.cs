using Runner.Enums;
using Runner.Profile;
using Runner.Services;
using Runner.Services.ADS;
using UnityEngine;

namespace Runner 
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InputType _inputType;
        [SerializeField] private float _playerSpeed;
        [SerializeField] private float _playerJumpHeight;
        [SerializeField] private float _playerHealth;
        [SerializeField] private Transform _placeForUi;

        private const GameState InitialState = GameState.Start;
        
        private MainContoller _mainContoller;

        private BaseAdsProvider _interstitialProvider;

        void Start()
        {
            var profilePlayer = new ProfilePlayer(_inputType, _playerSpeed, _playerJumpHeight, _playerHealth, InitialState);
            _mainContoller = new MainContoller(_placeForUi, profilePlayer);

            _interstitialProvider = new InterstitialAdsProvider();
            _interstitialProvider.Subscribe();

            if (ServicesHandler.AdsService.IsInitialized) OnAdsInitialized();
            else ServicesHandler.AdsService.Initialized.AddListener(OnAdsInitialized);
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

