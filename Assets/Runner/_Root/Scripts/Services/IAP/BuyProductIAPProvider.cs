namespace Runner.Services.IAP
{
    internal class BuyProductIAPProvider : BaseIAPProvider
    {
        public BuyProductIAPProvider() { }

        public override void Subscribe()
        {
            ServicesHandler.IAPService.PurchaseSucceed.AddListener(OnIAPSucceed);
            ServicesHandler.IAPService.PurchaseFailed.AddListener(OnIAPFailed);
        }

        public override void Unsubscribe()
        {
            ServicesHandler.IAPService.PurchaseSucceed.RemoveListener(OnIAPSucceed);
            ServicesHandler.IAPService.PurchaseFailed.RemoveListener(OnIAPFailed);
        }

        public override void Execute(string productId) => ServicesHandler.IAPService.Buy(productId);
    }
}
