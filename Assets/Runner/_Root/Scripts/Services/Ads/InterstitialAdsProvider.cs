namespace Runner.Services.ADS
{
    internal class InterstitialAdsProvider : BaseAdsProvider
    {
        public InterstitialAdsProvider() { }

        public override void Subscribe()
        {
            ServicesHandler.AdsService.InterstitialPlayer.Finished += ADSFinished;
            ServicesHandler.AdsService.InterstitialPlayer.Failed += ADSCanceled;
            ServicesHandler.AdsService.InterstitialPlayer.Skipped += ADSCanceled;
        }

        public override void Unsubscribe()
        {
            ServicesHandler.AdsService.InterstitialPlayer.Finished -= ADSFinished;
            ServicesHandler.AdsService.InterstitialPlayer.Failed -= ADSCanceled;
            ServicesHandler.AdsService.InterstitialPlayer.Skipped -= ADSCanceled;
        }

        public override void Execute() => ServicesHandler.AdsService.InterstitialPlayer.Play();
    }
}
