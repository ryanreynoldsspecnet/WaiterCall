using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WaiterCall.API.Signalr
{
    [Authorize]  // Require authentication for the hub
    public class WaiterHub : Hub
    {
        public async Task NotifyWaiter(string waiterId, string message)
        {
            // Notify a specific waiter
            await Clients.User(waiterId).SendAsync("ReceiveNotification", message);
        }

        public async Task NotifyAllWaiters(string message)
        {
            // Notify all waiters
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
