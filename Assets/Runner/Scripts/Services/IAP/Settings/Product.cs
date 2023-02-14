using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAP.Settings
{
    [Serializable]
    internal class IAPPayot
    {
        [field : SerializeField] public PayoutType payoutType { get; private set; }
        [field: SerializeField] public string subType { get; private set; }
        [field: SerializeField] public double quantity { get; private set; }
    }

    [Serializable]
    internal class Product
    {
        [SerializeField] private string _id;
        [SerializeField] private ProductType _productType;
        [SerializeField] private IAPPayot[] _payots;

        public string Id => _id;
        public ProductType ProductType => _productType;
        public IReadOnlyCollection<IAPPayot> Payots => _payots;
    }
}
