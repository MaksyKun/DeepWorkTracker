using DeepWorkTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepWorkTracker.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<DeepWorkSession> DeepWorkSessions => Set<DeepWorkSession>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
