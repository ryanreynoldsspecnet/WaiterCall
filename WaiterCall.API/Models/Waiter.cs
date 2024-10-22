namespace WaiterCall.API.Models
{
    public class Waiter
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }  // store hashed password
        public bool IsLoggedIn { get; set; }
    }
}
