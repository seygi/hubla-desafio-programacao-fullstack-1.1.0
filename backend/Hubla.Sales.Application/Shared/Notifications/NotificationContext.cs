using System.Net;

namespace Hubla.Sales.Application.Shared.Notifications
{
    public class NotificationContext : INotificationContext
    {
        private NotificationErrors _notifications;
        private HttpStatusCode _httpStatusCode;

        public bool HasNotifications => _notifications.Errors.Any();
        public NotificationErrors Notifications => _notifications;
        public HttpStatusCode HttpStatusCode => _httpStatusCode;

        public NotificationContext()
        {
            _notifications = NotificationErrors.Empty;
        }

        public void Create(HttpStatusCode httpStatusCode, NotificationErrors notificationErrors)
        {
            _httpStatusCode = httpStatusCode;
            _notifications = notificationErrors;
        }

        public void Create(HttpStatusCode httpStatusCode, string notificationErrors)
        {
            _httpStatusCode = httpStatusCode;
            _notifications.Add(string.Empty, notificationErrors);
        }

        public void Create(HttpStatusCode httpStatusCode)
        {
            _httpStatusCode = httpStatusCode;
        }

        //public void AddNotification(string key, string message)
        //{
        //    _notifications.Add(new Notification(key, message));
        //}

        //public void AddNotification(Notification notification)
        //{
        //    _notifications.Add(notification);
        //}

        //public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        //{
        //    _notifications.AddRange(notifications);
        //}

        //public void AddNotifications(IList<Notification> notifications)
        //{
        //    _notifications.AddRange(notifications);
        //}

        //public void AddNotifications(ICollection<Notification> notifications)
        //{
        //    _notifications.AddRange(notifications);
        //}

        //public void AddNotifications(ValidationResult validationResult)
        //{
        //    foreach (var error in validationResult.Errors)
        //    {
        //        AddNotification(error.ErrorCode, error.ErrorMessage);
        //    }
        //}
    }
}
