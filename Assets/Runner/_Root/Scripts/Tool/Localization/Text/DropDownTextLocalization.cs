using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runner.Tool.Localization
{
    internal class DropDownTextLocalization : TextLocalization
    {
        [SerializeField] private TMP_Dropdown _dropDown;

        [Header("DropDownText Settings")]
        [SerializeField] private string _tableName;
        [SerializeField] private string _selectedLocalizationTag;
        [SerializeField] private string _optionsLocalizationTag;


        private void OnValidate()
        {
            _dropDown ??= GetComponent<TMP_Dropdown>();
        }

        protected override void ChangeFont()
        {
            LocalizationSettings.AssetDatabase.GetTableAsync(tableFontName).Completed +=
             handle =>
             {
                 if (handle.Status == AsyncOperationStatus.Succeeded)
                 {
                     AssetTable table = handle.Result;
                    _dropDown.itemText.font = table.GetAssetAsync<TMP_FontAsset>(localizationFontTag).Result;
                    _dropDown.captionText.font = table.GetAssetAsync<TMP_FontAsset>(localizationFontTag).Result;
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
                      string[] dropValues = table.GetEntry(_optionsLocalizationTag)?.GetLocalizedString().Split(";");
                      int i = 0;
                      foreach(var option in _dropDown.options)
                      {
                          option.text = dropValues[i];
                          i++;
                      }

                      _dropDown.captionText.text = table.GetEntry(_selectedLocalizationTag)?.GetLocalizedString();
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
