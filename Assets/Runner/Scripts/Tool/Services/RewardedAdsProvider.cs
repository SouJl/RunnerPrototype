using Runner.Services;

namespace Runner.Scripts.Tool
{
    internal class RewardedAdsProvider : BaseAdsProvider
    {
        public RewardedAdsProvider() { }

        public override void Subscribe()
        {
            ServicesHandler.AdsService.RewardedPlayer.Finished += ADSFinished;
            ServicesHandler.AdsService.RewardedPlayer.Failed += ADSCanceled;
            ServicesHandler.AdsService.RewardedPlayer.Skipped += ADSCanceled;
        }

        public override void Unsubscribe()
        {
            ServicesHandler.AdsService.RewardedPlayer.Finished -= ADSFinished;
            ServicesHandler.AdsService.RewardedPlayer.Failed -= ADSCanceled;
            ServicesHandler.AdsService.RewardedPlayer.Skipped -= ADSCanceled;
        }
        
        public override void Execute() => ServicesHandler.AdsService.RewardedPlayer.Play();
    }
}
