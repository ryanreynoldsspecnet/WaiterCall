using Microsoft.EntityFrameworkCore;
using WaiterCall.API.Models;

namespace WaiterCall.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Add DbSets for your entities
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<OrderNotification> OrderNotifications { get; set; }
    }
}
