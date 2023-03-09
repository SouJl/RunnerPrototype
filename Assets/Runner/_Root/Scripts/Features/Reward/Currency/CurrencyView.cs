using UnityEngine;

namespace Runner.Features.Reward
{
    internal interface ICurrencyView
    {
        void Init(int schemeCount, int diamondCount);
        void AddScheme(int value);
        void AddDiamond(int value); 
    }

    internal class CurrencyView :MonoBehaviour, ICurrencyView
    {
        [SerializeField] private CurrencySlotView _schemeCurrency;
        [SerializeField] private CurrencySlotView _diamondCurrency;

        public void Init(int schemeCount, int diamondCount)
        {
            AddScheme(schemeCount);
            AddDiamond(diamondCount);
        }

        public void AddScheme(int value) => _schemeCurrency.SetData(value);
        public void AddDiamond(int value) => _diamondCurrency.SetData(value);

    }
}
