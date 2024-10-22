namespace WaiterCall.API.Models
{
    public class OrderNotification
    {
        public int Id { get; set; }
        public required string Message { get; set; }
        public int WaiterId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDelivered { get; set; }

        // Navigation property
        public Waiter ?Waiter { get; set; }
    }
}
