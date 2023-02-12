using Runner.Scripts.Enums;
using Runner.Scripts.Interfaces;
using Runner.Scripts.Profile;
using Runner.Scripts.Tool;
using Runner.Scripts.View;
using Runner.Services;
using UnityEngine;

namespace Runner.Scripts.Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/MainMenuUi");
        private readonly ProfilePlayer _profilePlayer;
        private MainMenuView _view;

        private IAdsProvider _rewardedAdsProvider;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer) 
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSettings, Exit, PlayRewardedADS);

            _rewardedAdsProvider = new RewardedAdsProvider();

            _rewardedAdsProvider.OnADSFinished += RewardedAdsFinished;
            _rewardedAdsProvider.OnADSCanceled += RewardedAdsCanceled;

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
            _rewardedAdsProvider.UnsubscribeADS();
            _rewardedAdsProvider.OnADSFinished -= RewardedAdsFinished;
            _rewardedAdsProvider.OnADSCanceled -= RewardedAdsCanceled;
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void OpenSettings() 
        {
            _profilePlayer.CurrentState.Value = GameState.Settings;
        }

        private void Exit() 
        {
            _profilePlayer.CurrentState.Value = GameState.Exit;
        }


        private void PlayRewardedADS() => _rewardedAdsProvider.Execute();


        private void RewardedAdsFinished()
        {
            Debug.Log("Received a reward for ads!");
        }

        private void RewardedAdsCanceled()
        {
            Debug.Log("Receiving a reward for ads has been interrupted!");
        }
    }
}


