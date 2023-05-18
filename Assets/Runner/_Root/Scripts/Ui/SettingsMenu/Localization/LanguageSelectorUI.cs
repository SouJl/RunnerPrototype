using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

namespace Runner.UI.Localization
{
    internal enum LanguageType 
    {
        English =0,
        Japanese = 1,
        Russian = 2,
    }

    internal class LanguageSelectorUI : MonoBehaviour
    {
        private Dictionary<string, LanguageType> _languageByType = new Dictionary<string, LanguageType>()
        {
            {"en",LanguageType.English},
            {"ja",LanguageType.Japanese},
            {"ru",LanguageType.Russian},
        };
        private readonly string[] _languages = new string[] { "English", "Japanese", "Russian"};

        [SerializeField] private TMP_Dropdown _dropDown;

        private void Awake()
        {
            foreach (var language in _languages) 
            {
                _dropDown.options.Add(new TMP_Dropdown.OptionData { text = language});
            }
            _dropDown.onValueChanged.AddListener(ChangeLanguageHandle);

            _dropDown.value = (int)_languageByType[PlayerPrefs.GetString("selected-locale")];
        }

        private void OnDestroy()
        {
            _dropDown.onValueChanged.RemoveAllListeners();
        }

        public void ChangeLanguageHandle(int languageValue) 
        {
            var language = (LanguageType)languageValue;
            ChangeLanguage((int)language);
        }

        private void ChangeLanguage(int index) 
            => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
