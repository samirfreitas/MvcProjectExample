﻿using System.Linq;

namespace Example.Business.Notifications
{
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
        }
        public string Message { get; }

    }
}