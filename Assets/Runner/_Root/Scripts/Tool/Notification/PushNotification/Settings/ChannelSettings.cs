using System;
using UnityEngine;

namespace Runner.Tool.Notification.Push.Settings
{
    [Serializable]
    internal class ChannelSettings
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [SerializeField] private Importance _importance;

#if UNITY_ANDROID
        public Unity.Notifications.Android.Importance Importance 
            => (Unity.Notifications.Android.Importance)_importance;
#else
        public Importance Importance => _importance;
#endif
    }

    internal enum Importance
    {
        None = 0,
        Low = 2,
        Default = 3,
        High = 4,
    }
}
