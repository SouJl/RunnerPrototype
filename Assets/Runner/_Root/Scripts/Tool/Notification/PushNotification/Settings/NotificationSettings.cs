using UnityEngine;

namespace Runner.Tool.Notification.Push.Settings
{
    [CreateAssetMenu(fileName = nameof(NotificationSettings), 
        menuName = "Settings/Notifications/" + nameof(NotificationSettings))]
    internal class NotificationSettings : ScriptableObject
    {
        [field: SerializeField] public ChannelSettings[] Channels { get; private set; }
        [field: SerializeField] public NotificationData[] Notifications { get; private set; }
    }
}
