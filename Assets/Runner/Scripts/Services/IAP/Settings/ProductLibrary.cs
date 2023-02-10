using UnityEngine;

namespace Services.IAP.Settings
{
    [CreateAssetMenu(fileName = nameof(ProductLibrary), menuName = "Settings/IAP/" + nameof(ProductLibrary))]
    internal class ProductLibrary:ScriptableObject
    {
        [SerializeField] private Product[] _products;

        public Product[] Products => _products;
    }
}
