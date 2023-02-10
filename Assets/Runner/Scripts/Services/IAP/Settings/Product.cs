using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAP.Settings
{
    [Serializable]
    internal class Product
    {
        [SerializeField] private string _id;
        [SerializeField] private ProductType _productType;

        public string Id => _id;
        public ProductType ProductType => _productType;
    }
}
