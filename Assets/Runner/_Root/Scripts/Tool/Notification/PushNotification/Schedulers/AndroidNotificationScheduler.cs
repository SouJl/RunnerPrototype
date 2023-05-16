using System;
using Runner.Tool.Notification.Push.Settings;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

namespace Runner.Tool.Notification.Push
{
    internal class AndroidNotificationScheduler : INotificationScheduler
    {
        public void ScheduleNotification(NotificationData notificationData)
        {
#if UNITY_ANDROID
            AndroidNotification androidNotification = CreateAndroidNotification(notificationData);
            AndroidNotificationCenter.SendNotification(androidNotification, notificationData.Id);
#endif
        }

        public void RegisterChannel(ChannelSettings channelSettings)
        {
#if UNITY_ANDROID
            var androidNotificationChannel = new AndroidNotificationChannel
            (
                channelSettings.Id,
                channelSettings.Name,
                channelSettings.Description,
                channelSettings.Importance
            );

            AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);
#endif
        }

#if UNITY_ANDROID
        private AndroidNotification CreateAndroidNotification(NotificationData notificationData)
        {
            return notificationData.RepeatType switch
            {
                NotificationRepeat.Once => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    notificationData.FireTime
                ),

                NotificationRepeat.Repeatable => new AndroidNotification
                (
                    notificationData.Title,
                    notificationData.Text,
                    notificationData.FireTime,
                    notificationData.RepeatInterval
                ),

                _ => throw new ArgumentOutOfRangeException(nameof(notificationData.RepeatType))
            };
        }
#endif
    }
}
