using System;
using UnityEngine;

namespace Services.Ads.UnityAds.Settings
{
    [Serializable]
    internal class AdsPlayerSettings
    {
        [SerializeField] private bool _enabled;
        [SerializeField] private string _androidId;
        [SerializeField] private string _iosId;

        public bool Enabled => _enabled;

        public string Id =>
#if UNITY_EDITOR
            _androidId;
#else
            Application.platform switch
            {
                RuntimePlatform.Android => _androidId,
                RuntimePlatform.IPhonePlayer => _iosId,
                _ => ""
            };
#endif

    }
}
