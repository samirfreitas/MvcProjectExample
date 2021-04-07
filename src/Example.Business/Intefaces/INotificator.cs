using Example.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Business.Intefaces
{
    public interface INotificator
    {
        bool HaveNotication();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
