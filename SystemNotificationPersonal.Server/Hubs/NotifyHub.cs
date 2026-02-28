using Microsoft.AspNetCore.SignalR;

namespace SystemNotificationPersonal.Server.Hubs
{
    public class NotifyHub : Hub
    {
        public async Task NotifyWorker(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
