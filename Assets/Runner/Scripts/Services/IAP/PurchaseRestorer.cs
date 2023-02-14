using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    internal class PurchaseRestorer
    {
        private readonly IExtensionProvider _extensionProvider;

        public PurchaseRestorer(IExtensionProvider extensionProvider) =>
            _extensionProvider = extensionProvider;

        public void Restore()
        {
            Log("RestorePurchase Started ...");

            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.OSXPlayer:
                    {       
                        _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnRestoredTransactions);
                    break;
                    }
                case RuntimePlatform.Android: 
                    {
                        _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnRestoredTransactions);
                        break;
                    }
                default: 
                    {
                        Log($"RestorePurchase FAIL. Not supported on this platform. Current = {Application.platform}");
                        break;
                    }
            }
        }

        private void OnRestoredTransactions(bool result, string error) 
        {
            if (result)
            {
                Log($"RestorePurchase continuing. If no further messages, no purchases available to restore.");
            }
            else
            {
                Log($"RestorePurchase failed: {error}");
            }
        }

        private void Log(string message) => Debug.Log(WrapMessage(message));

        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
