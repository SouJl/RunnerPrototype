using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    internal class PurchaseValidator
    {
        public bool Validate(PurchaseEventArgs args) 
        {
            bool isValid = true;
            
            //Предваритльно необходимо дабавить данные из магазине прежде чем разкомментировать
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            //var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.bundleIdentifier);
            //try
            //{
            //    var result = validator.Validate(args.purchasedProduct.receipt);
            //}
            //catch (IAPSecurityException)
            //{
            //    isValid = false;
            //}            
#endif
            string logMessage = isValid
                ? $"Receipt is valid. Contents: {args.purchasedProduct.receipt}"
                : "Invalid receipt, not unlocking content";

            Log(logMessage);

            return isValid;
        }

        private void Log(string message) => Debug.Log(WrapMessage(message));

        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
