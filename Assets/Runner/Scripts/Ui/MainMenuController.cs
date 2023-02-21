using UnityEngine;
using Runner.Enums;
using Runner.Profile;
using Runner.Tool;
using Runner.View;
using Runner.Services;
using Runner.Services.ADS;
using Runner.Services.IAP;

namespace Runner.Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/MainMenuUi");
        private readonly ProfilePlayer _profilePlayer;
        private MainMenuView _view;

        private BaseAdsProvider _rewardedAdsProvider;
        private BaseIAPProvider _buyProductProvdier;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSettings, OpenStorage, Exit, PlayRewardedADS, BuyProduct);

            _rewardedAdsProvider = new RewardedAdsProvider();
            _buyProductProvdier = new BuyProductIAPProvider();

            SubscribeServices();

            ServicesHandler.Analytics.SendMainMenuOpen();
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
            AddGameObject(objectView);
            return objectView.GetComponent<MainMenuView>();
        }

        protected override void OnDispose()
        {
            UnsubscribeServices();
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void OpenSettings() 
        {
            _profilePlayer.CurrentState.Value = GameState.Settings;
        }

        private void OpenStorage() 
        {
            _profilePlayer.CurrentState.Value = GameState.Storage;
        }

        private void Exit() 
        {
            _profilePlayer.CurrentState.Value = GameState.Exit;
        }

        #region In MainMenu services methods

        private void SubscribeServices() 
        {
            _rewardedAdsProvider.Subscribe();
            _rewardedAdsProvider.OnTrueResult += RewardedAdsFinished;
            _rewardedAdsProvider.OnFalseResult += RewardedAdsCanceled;

            _buyProductProvdier.Subscribe();
            _buyProductProvdier.OnTrueResult += BuyProductSucceeded;
            _buyProductProvdier.OnFalseResult += BuyProductFailed;
        }

        private void UnsubscribeServices() 
        {
            _rewardedAdsProvider.Unsubscribe();
            _rewardedAdsProvider.OnTrueResult -= RewardedAdsFinished;
            _rewardedAdsProvider.OnFalseResult -= RewardedAdsCanceled;

            _buyProductProvdier.Unsubscribe();
            _rewardedAdsProvider.OnTrueResult -= BuyProductSucceeded;
            _rewardedAdsProvider.OnFalseResult -= BuyProductFailed;
        }

        private void PlayRewardedADS() => _rewardedAdsProvider.Execute();

        private void BuyProduct(string buyProductId) => _buyProductProvdier.Execute(buyProductId);

        private void RewardedAdsFinished()
        {
            Debug.Log("Received a reward for ads!");
        }

        private void RewardedAdsCanceled()
        {
            Debug.Log("Receiving a reward for ads has been interrupted!");
        }

        private void BuyProductSucceeded()
        {
            Debug.Log("Purchase succeed");
        }

        private void BuyProductFailed()
        {
            Debug.Log("Purchase failed");
        }

        #endregion
    }
}


