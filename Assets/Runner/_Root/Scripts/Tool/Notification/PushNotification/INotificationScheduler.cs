using Runner.Tool.Notification.Push.Settings;

namespace Runner.Tool.Notification.Push 
{
    internal interface INotificationScheduler
    {
        void ScheduleNotification(NotificationData notificationData);
    }
}
