using Services.IAP;
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

        public void SendTransaction(string productId, long amount, string currency, IEnumerable<IAPPayot> payots)
        {
            var productsReceived = new Product();
            productsReceived.Items = new List<Item>();

            foreach (var payot in payots) 
            {
                productsReceived.Items.Add(new Item() { ItemName = payot.subType, ItemType = payot.payoutType.ToString(), ItemAmount = (long)payot.quantity });
            }

            var productsSpent = new Product()
            {
                RealCurrency = new RealCurrency() { RealCurrencyType = currency, RealCurrencyAmount = amount }
            };

            AnalyticsService.Instance.Transaction(new TransactionParameters
            {
                ProductID = productId,
                ProductsReceived = productsReceived,
                ProductsSpent = productsSpent,
                TransactionName = $"Test transaction - {productId}",
                TransactionType = TransactionType.PURCHASE,
            });
        }
    }
}
