using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAP.Settings
{
    internal interface IiapPayot 
    {
        PayoutType payoutType { get; }
        string subType { get; }
        long quantity { get; }
    }

    [Serializable]
    internal class IAPPayot : IiapPayot
    {
        [field : SerializeField] public PayoutType payoutType { get; private set; }
        [field: SerializeField] public string subType { get; private set; }
        [field: SerializeField] public long quantity { get; private set; }
    }

    [Serializable]
    internal class Product
    {
        [SerializeField] private string _id;
        [SerializeField] private ProductType _productType;
        [SerializeField] private IAPPayot[] _payots;

        public string Id => _id;
        public ProductType ProductType => _productType;
        public IReadOnlyCollection<IiapPayot> Payots => _payots;
    }
}
