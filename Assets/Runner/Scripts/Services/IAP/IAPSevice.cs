using Services.IAP.Settings;
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
            _purchaseRestorer = new PurchaseRestorer(_extensionProvider);

            Log("IAP Initialized");
        }
        
        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            IsInitialized = true;
            Error("IAP Initialization Failed");
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (_purchaseValidator.Validate(purchaseEvent)) 
            {
                PurchaseSucceed?.Invoke();
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
