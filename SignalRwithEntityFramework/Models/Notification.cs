using System;
using System.Collections.Generic;

namespace SignalRwithEntityFramework.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string MessageType { get; set; } = null!;
        public DateTime NotificationDateTime { get; set; }
    }
}
