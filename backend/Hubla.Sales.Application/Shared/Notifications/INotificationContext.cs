using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Hubla.Sales.Application.Shared.Notifications
{
    [ExcludeFromCodeCoverage]
    public interface INotificationContext
    {
        NotificationErrors Notifications { get; }
        HttpStatusCode HttpStatusCode { get; }
        bool HasNotifications { get; }

        void Create(HttpStatusCode httpStatusCode, NotificationErrors notificationErrors);
        void Create(HttpStatusCode httpStatusCode, string notificationErrors);
        void Create(HttpStatusCode httpStatusCode);
    }
}
