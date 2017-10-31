using FPSim.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("project", "public");
            modelBuilder.Entity<Project>().HasOne(e => e.Application);
            modelBuilder.Entity<Project>().HasOne(e => e.User);
            modelBuilder.Entity<Project>().HasMany(e => e.Scenarios);

            modelBuilder.Entity<Scenario>().ToTable("scenario", "public");
            modelBuilder.Entity<Scenario>().HasOne(e => e.Project).WithMany(e => e.Scenarios);
        }
    }
}
