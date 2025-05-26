using Microsoft.EntityFrameworkCore;
using WorkshopManager.Models;

namespace WorkshopManager.Models
{
    public class WorkshopContext : DbContext
    {
        public WorkshopContext(DbContextOptions<WorkshopContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

