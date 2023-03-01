using Runner.Services;
using Services.IAP.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    internal class IAPSevice : MonoBehaviour, IIAPService, IStoreListener
    {
        [Header("Components")]
        [SerializeField] private ProductLibrary _productLibrary;

        [Header("Events")]
        [SerializeField] private UnityEvent _initialized;
        [SerializeField] private UnityEvent _purchaseSucceed;
        [SerializeField] private UnityEvent _purchaseFailed;

        public UnityEvent Initialized => _initialized;

        public UnityEvent PurchaseSucceed => _purchaseSucceed;

        public UnityEvent PurchaseFailed => _purchaseFailed;

        public bool IsInitialized { get; private set; }

        private IExtensionProvider _extensionProvider;
        private PurchaseValidator _purchaseValidator;
        private PurchaseRestorer _purchaseRestorer;
        private IStoreController _controller;

        private void Awake()
        {
            InitializeProducts();
        }

        private void InitializeProducts()
        {
            var purchasingModule = StandardPurchasingModule.Instance();
            var builder = ConfigurationBuilder.Instance(purchasingModule);

            foreach (var product in _productLibrary.Products)
            {
                var payots = new List<PayoutDefinition>();
                foreach (var payot in product.Payots)
                {
                    payots.Add(new PayoutDefinition(payot.payoutType, payot.subType, payot.quantity));
                }
                builder.AddProduct(product.Id, product.ProductType, null, payots);
            }

            Log("Products initialized");
            UnityPurchasing.Initialize(this, builder);

        }

        void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            IsInitialized = true;
            _controller = controller;
            _extensionProvider = extensions;
            _purchaseValidator = new PurchaseValidator();
            _purchaseRestorer = new PurchaseRestorer(_extensionProvider);

            Log("IAP Initialized");
        }

        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            IsInitialized = true;
            Error("IAP Initialization Failed");
        }

        void IStoreListener.OnInitializeFailed(InitializationFailureReason error, string message)
        {
            IsInitialized = true;
            Error($"IAP Initialization Failed with message: {message}");
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (_purchaseValidator.Validate(purchaseEvent))
            {
                OnPurchaseSucceed(purchaseEvent.purchasedProduct);
            }
            else
            {
                OnPurchaseFailed(purchaseEvent.purchasedProduct.definition.id, "NonValid");
            }
            return PurchaseProcessingResult.Complete;
        }

        void IStoreListener.OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
        {
            OnPurchaseFailed(product.definition.id, failureReason.ToString());
        }

        private void OnPurchaseSucceed(UnityEngine.Purchasing.Product product)
        {
            string productId = product.definition.id;
            string currency = product.metadata.isoCurrencyCode;
            decimal currencyAmount = product.metadata.localizedPrice;

            ServicesHandler.Analytics.SendTransaction(productId, (long)currencyAmount, currency, product.definition.payouts);

            Log($"Purchased: {productId}. Payots: {string.Join(" , ", product.definition.payouts.Select(p => p.subtype))}");
            PurchaseSucceed?.Invoke();
        }

        private void OnPurchaseFailed(string productId, string reason)
        {
            Error($"Failed {productId}: {reason}");
            PurchaseFailed?.Invoke();
        }

        public void Buy(string id)
        {
            if (IsInitialized)
            {
                _controller.InitiatePurchase(id);
            }
            else
            {
                Error($"Buy {id} FAIL. Not Initialized");
            }
        }

        public string GetCost(string productID)
        {
            var product = _controller.products.WithID(productID);
            return product != null ? product.metadata.localizedPriceString : "N/A";
        }

        public void RestorePurchases()
        {
            if (IsInitialized)
            {
                _purchaseRestorer.Restore();
            }
            else
            {
                Error("RestorePurchases FAIL. Not Initialized");
            }
        }

        private void Log(string message) => Debug.Log(WrapMessage(message));

        private void Error(string message) => Debug.LogError(WrapMessage(message));

        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";


    }
}
