using UnityEngine;

namespace Services.Ads.UnityAds.Settings
{
    [CreateAssetMenu(fileName = nameof(UnityAdsSettings), menuName = "Settings/Ads/" + nameof(UnityAdsSettings))]
    internal class UnityAdsSettings:ScriptableObject
    {
        [Header("Game ID")]
        [SerializeField] private string _androidGameId;
        [SerializeField] private string _iosGameId;

        [Header("Players")]
        [SerializeField] private AdsPlayerSettings _interstitial;
        [SerializeField] private AdsPlayerSettings _rewarded;
        [SerializeField] private AdsPlayerSettings _banner;

        [Header("Settings")]
        [SerializeField] private bool _testMode = true;
        [SerializeField] private bool _eneblePerPlacementMode = true;


        internal AdsPlayerSettings Interstitial => _interstitial;
        internal AdsPlayerSettings Rewarded => _rewarded;
        internal AdsPlayerSettings Banner => _banner;

        public bool TestMode => _testMode;
        public bool EneblePerPlacementMode => _eneblePerPlacementMode;

        public string GameId =>
#if UNITY_EDITOR
            _androidGameId;
#else
            Application.platform switch
            {
                RuntimePlatform.Android => _androidGameId,
                RuntimePlatform.IPhonePlayer => _iosGameId,
                _ => ""
            };
#endif

    }
}
