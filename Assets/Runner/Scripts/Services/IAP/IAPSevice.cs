using Services.IAP.Settings;
using System;
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
            foreach(var product in _productLibrary.Products)
            {
                builder.AddProduct(product.Id, product.ProductType);
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
            _purchaseRestorer = new PurchaseRestorer();

            Log("IAP Initialized");
        }
        
        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            IsInitialized = true;
            Error("IAP Initialization Failed");
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            return PurchaseProcessingResult.Complete;
        }

        void IStoreListener.OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
        {
            OnPurchaseFailed(product.definition.id, failureReason.ToString());
        }

        private void OnPurchaseFailed(string productId, string reason)
        {
            Error($"Failed {productId}: {reason}");
            PurchaseFailed?.Invoke();
        }

        public void Buy(string id)
        {
            
        }

        public string GetCost(string productID)
        {
            throw new System.NotImplementedException();
        }

        public void RestorePurchases()
        {
            
        }

        private void Log(string message) => Debug.Log(WraMessage(message));

        private void Error(string message) => Debug.LogError(WraMessage(message));

        private string WraMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
