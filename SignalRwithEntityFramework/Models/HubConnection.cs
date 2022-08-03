using System;
using System.Collections.Generic;

namespace SignalRwithEntityFramework.Models
{
    public partial class HubConnection
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
