using System.Collections.Generic;
using Unity.Services.Analytics;
namespace Services.Analytics
{
    internal class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName, Dictionary<string, object> eventData)
            => AnalyticsService.Instance.CustomData(eventName, eventData);

        public void SendEvent(string eventName)
        {
            AnalyticsService.Instance.CustomData(eventName, new Dictionary<string, object>());
        }

        public void SendTransaction(string productId, long amount, string currency) 
        {

            var productsSpent = new Product()
            {
                RealCurrency = new RealCurrency() { RealCurrencyType = currency, RealCurrencyAmount = amount}
            };

            AnalyticsService.Instance.Transaction(new TransactionParameters
            {
                ProductID = productId,
                ProductsSpent = productsSpent,
                TransactionName = "",
                TransactionType = TransactionType.PURCHASE,
            });
        }
    }
}
