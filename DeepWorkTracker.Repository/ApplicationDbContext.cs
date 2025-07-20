using DeepWorkTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepWorkTracker.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public DbSet<DeepWorkSession> DeepWorkSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
