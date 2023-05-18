using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runner.Tool.Localization
{
    internal class TextLocalizationByApi : MonoBehaviour
    {
        private readonly string _tableFontName = "Fonts";
        private readonly string _localizationFontTag = "text_font";

        [SerializeField] private TMP_Text _changeText;

        [Header("Settings")]
        [SerializeField] private string _tableName;
        [SerializeField] private string _localizationTag;

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
            
            ChangeFont();
            ChangeText();
        }

        private void ChangeText()
        {
            LocalizationSettings.StringDatabase.GetTableAsync(_tableName).Completed += 
                handle => 
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        StringTable table = handle.Result;
                        _changeText.text = table.GetEntry(_localizationTag)?.GetLocalizedString();
                    }
                    else
                    {
                        string errorMessage = $"[{GetType().Name}] Could not load String Table: {handle.OperationException}";
                        Debug.LogError(errorMessage);
                    }
                };
        }

        private void ChangeFont()
        {
            LocalizationSettings.AssetDatabase.GetTableAsync(_tableFontName).Completed +=
               handle =>
               {
                   if (handle.Status == AsyncOperationStatus.Succeeded)
                   {
                       AssetTable table = handle.Result;
                       _changeText.font = table.GetAssetAsync<TMP_FontAsset>(_localizationFontTag).Result;
                   }
                   else
                   {
                       string errorMessage = $"[{GetType().Name}] Could not load Asset Table: {handle.OperationException}";
                       Debug.LogError(errorMessage);
                   }
               };
        }

    }
}
