using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal class RewardedPlayer : UnityAdsPlayer
    {
        public RewardedPlayer(string id) : base(id) { }

        protected override void Load() => Advertisement.Load(Id);

        protected override void OnPlaying() => Advertisement.Show(Id);
    }
}
