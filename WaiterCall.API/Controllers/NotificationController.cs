using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WaiterCall.API.Signalr;

namespace WaiterCall.API.Controllers
{
    [Authorize]  // Require authentication for the API controller
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<WaiterHub> _hubContext;

        public NotificationController(IHubContext<WaiterHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // Endpoint to notify a specific waiter
        [HttpPost("notify-waiter")]
        public async Task<IActionResult> NotifyWaiter(string waiterId, string message)
        {
            // Send a message to the specific waiter
            await _hubContext.Clients.User(waiterId).SendAsync("ReceiveNotification", message);
            return Ok(new { Message = $"Notified waiter {waiterId}" });
        }

        // Endpoint to notify all waiters
        [HttpPost("notify-all")]
        public async Task<IActionResult> NotifyAllWaiters(string message)
        {
            // Broadcast a message to all connected waiters
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
            return Ok(new { Message = "Notified all waiters" });
        }
    }
}
