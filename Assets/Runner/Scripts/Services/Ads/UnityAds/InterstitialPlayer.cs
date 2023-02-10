using System;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal class InterstitialPlayer : UnityAdsPlayer
    {
        public InterstitialPlayer(string id) : base(id)
        {
        }

        protected override void Load() => Advertisement.Load(Id);

        protected override void OnPlaying() => Advertisement.Show(Id);
    }
}
