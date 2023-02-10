namespace Services.Ads.UnityAds
{
    internal class StubPlayer : UnityAdsPlayer
    {
        public StubPlayer(string id) : base(id) { }

        protected override void Load() { }

        protected override void OnPlaying() { }
    }
}
