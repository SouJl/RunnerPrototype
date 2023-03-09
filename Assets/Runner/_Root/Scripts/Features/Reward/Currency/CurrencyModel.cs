using System;
using UnityEngine;

namespace Runner.Features.Reward
{
    internal interface ICurrencyModel
    {
        event Action SchemeChanged;
        event Action DiamondChanged;

        int Scheme { get; set; }
        int Diamond { get; set; }
    }

    internal class CurrencyModel : ICurrencyModel
    {
        private const string SchemeKey = nameof(SchemeKey);
        private const string DiamondKey = nameof(DiamondKey);

        public event Action SchemeChanged;
        public event Action DiamondChanged;


        public int Scheme
        {
            get => GetValue(SchemeKey, 0);
            set => SetValue(SchemeKey, Scheme, value, SchemeChanged);
        }

        public int Diamond
        {
            get => GetValue(DiamondKey, 0);
            set => SetValue(DiamondKey, Diamond, value, DiamondChanged);
        }


        private int GetValue(string valueKey, int defaultValue = 0) =>
            PlayerPrefs.GetInt(valueKey, defaultValue);

        private void SetValue(string valueKey, int oldValue, int newValue, Action changedAction)
        {
            if (oldValue == newValue)
                return;

            PlayerPrefs.SetInt(valueKey, newValue);
            changedAction?.Invoke();
        }
    }
}
