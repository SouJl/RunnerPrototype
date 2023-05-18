using Runner.Tool.Notification.Push.Settings;
using System.Linq;
using UnityEngine;

namespace Runner.Tool.Notification.Push
{
    internal interface IPushNotification 
    {
        void CreateNotification();

        void CreateNotificationById(string notificationId);
    }

    internal class PushNotificationProvider : MonoBehaviour, IPushNotification
    {
        [Header("Settings")]
        [SerializeField] private bool _isActive = true;
        [SerializeField] private NotificationSettings _settings;
   
        private INotificationScheduler _scheduler;

        private void Awake()
        {
            if(_settings == null)
            {
                _isActive = false;
                return;
            }
            
            var schedulerFactory = new NotificationSchedulerFactory(_settings);
            _scheduler = schedulerFactory.Create();
        }

        public void CreateNotification() 
        {
            if (_isActive == false) return;

            foreach (NotificationData notificationData in _settings.Notifications)
                _scheduler.ScheduleNotification(notificationData);
        }


        public void CreateNotificationById(string notificationId) 
        {
            var notification = _settings.Notifications.Where(n => n.Id == notificationId).FirstOrDefault();
            _scheduler.ScheduleNotification(notification);
        }

    }
}
