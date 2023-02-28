using UnityEngine;

namespace Rewards
{
    internal interface ICurrencyView 
    {
        void AddScheme(int value);
        void AddDiamond(int value);
    }

    internal class CurrencyView : MonoBehaviour, ICurrencyView
    {
        private const string SchemeKey = nameof(SchemeKey);
        private const string DiamondKey = nameof(DiamondKey);

        [SerializeField] private CurrencySlotView _schemeCurrency;
        [SerializeField] private CurrencySlotView _diamondCurrency;

        private int Scheme
        {
            get => PlayerPrefs.GetInt(SchemeKey);
            set => PlayerPrefs.SetInt(SchemeKey, value);
        }

        private int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey);
            set => PlayerPrefs.SetInt(DiamondKey, value);
        }

        private void Start()
        {
            _schemeCurrency.SetData(Scheme);
            _schemeCurrency.SetData(Diamond);
        }

        public void AddScheme(int value)
        {
            Scheme += value;
            _schemeCurrency.SetData(Scheme);
        }

        public void AddDiamond(int value)
        {
            Diamond += value;
            _schemeCurrency.SetData(Diamond);
        }  
    }
}
