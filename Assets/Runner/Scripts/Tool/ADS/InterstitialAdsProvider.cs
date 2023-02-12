using Runner.Services;

namespace Runner.Scripts.Tool
{
    internal class InterstitialAdsProvider : BaseAdsProvider
    {
        public InterstitialAdsProvider()
        {
            SubscribeADS();
        }

        public override void SubscribeADS()
        {
            ServicesHandler.AdsService.InterstitialPlayer.Finished += ADSFinished;
            ServicesHandler.AdsService.InterstitialPlayer.Failed += ADSCanceled;
            ServicesHandler.AdsService.InterstitialPlayer.Skipped += ADSCanceled;
        }

        public override void UnsubscribeADS()
        {
            ServicesHandler.AdsService.InterstitialPlayer.Finished -= ADSFinished;
            ServicesHandler.AdsService.InterstitialPlayer.Failed -= ADSCanceled;
            ServicesHandler.AdsService.InterstitialPlayer.Skipped -= ADSCanceled;
        }

        public override void Execute() => ServicesHandler.AdsService.InterstitialPlayer.Play();
    }
}
