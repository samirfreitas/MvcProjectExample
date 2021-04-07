using Example.Business.Intefaces;
using System.Collections.Generic;
using System.Linq;

namespace Example.Business.Notifications
{
    public class Notificator : INotificator
    {

        private List<Notification> _notifications;

        public Notificator()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }       

        public bool HaveNotication()
        {
            return _notifications.Any();
        }
    }
}
