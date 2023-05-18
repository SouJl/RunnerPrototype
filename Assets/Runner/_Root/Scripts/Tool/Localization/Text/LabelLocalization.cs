using UnityEngine;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

namespace Runner.Tool.Localization
{
    internal class LabelLocalization : TextLocalization
    {
        [SerializeField] private TMP_Text _localizedText;

        [Header("Label Settings")]
        [SerializeField] private string _tableName;
        [SerializeField] private string _localizationTag;

        private void OnEnable()
        {
            _localizedText ??= GetComponent<TMP_Text>();
        }

        protected override void ChangeFont()
        {
            LocalizationSettings.AssetDatabase.GetTableAsync(tableFontName).Completed +=
               handle =>
               {
                   if (handle.Status == AsyncOperationStatus.Succeeded)
                   {
                       AssetTable table = handle.Result;
                       _localizedText.font = table.GetAssetAsync<TMP_FontAsset>(localizationFontTag).Result;
                   }
                   else
                   {
                       string errorMessage = $"[{GetType().Name}] Could not load Asset Table: {handle.OperationException}";
                       Debug.LogError(errorMessage);
                   }
               };
        }

        protected override void ChangeText()
        {
            LocalizationSettings.StringDatabase.GetTableAsync(_tableName).Completed += 
                handle => 
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        StringTable table = handle.Result;
                        _localizedText.text = table.GetEntry(_localizationTag)?.GetLocalizedString();
                    }
                    else
                    {
                        string errorMessage = $"[{GetType().Name}] Could not load String Table: {handle.OperationException}";
                        Debug.LogError(errorMessage);
                    }
                };
        }
    }
}
