using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Runner.Tool.Localization
{
    internal abstract class TextLocalization : MonoBehaviour
    {
        protected readonly string tableFontName = "Fonts";
        protected readonly string localizationFontTag = "text_font";

    
        private void Awake()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            UpdateTextAsync();
        }

        
        private void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }


        private void OnSelectedLocaleChanged(Locale _) =>
            UpdateTextAsync();

        private void UpdateTextAsync()
        {
            ChangeText();
            ChangeFont();
        }

        protected abstract void ChangeFont();

        protected abstract void ChangeText();
    }
}
