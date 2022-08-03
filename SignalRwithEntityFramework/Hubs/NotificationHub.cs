using Microsoft.AspNetCore.SignalR;
using SignalRwithEntityFramework.Models;

namespace SignalRwithEntityFramework.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly SignalRWithEFContext dbContext;

        public NotificationHub(SignalRWithEFContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        public async Task SendNotificationToClient(string message, string username)
        {
            var hubConnections = dbContext.HubConnections.Where(con => con.Username == username).ToList();
            foreach (var hubConnection in hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
            }
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection(string username)
        {
            var connectionId = Context.ConnectionId;
            HubConnection hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                Username = username
            };

            dbContext.HubConnections.Add(hubConnection);
            await dbContext.SaveChangesAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = dbContext.HubConnections.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
            if(hubConnection != null)
            {
                dbContext.HubConnections.Remove(hubConnection);
                dbContext.SaveChangesAsync();
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
