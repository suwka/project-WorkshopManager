using Microsoft.EntityFrameworkCore;

namespace WorkshopManager.Models
{
    public class WorkshopContext : DbContext
    {
        public WorkshopContext(DbContextOptions<WorkshopContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}

