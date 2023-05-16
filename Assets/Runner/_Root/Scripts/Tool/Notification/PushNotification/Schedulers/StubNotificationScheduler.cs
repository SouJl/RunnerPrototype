using Runner.Tool.Notification.Push.Settings;
using UnityEngine;

namespace Runner.Tool.Notification.Push
{
    internal class StubNotificationScheduler : INotificationScheduler
    {
        public void ScheduleNotification(NotificationData notificationData) 
            => Debug.Log($"[{GetType()}] {notificationData}");
    }
}
