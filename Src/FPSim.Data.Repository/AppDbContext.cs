using FPSim.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


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
            // User
            modelBuilder.Entity<User>().ToTable("user", "public");
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();

            // Application
            modelBuilder.Entity<Application>().ToTable("application", "public");

            // Project
            modelBuilder.Entity<Project>().ToTable("project", "public");
            modelBuilder.Entity<Project>().HasOne(e => e.Application);
            modelBuilder.Entity<Project>().HasOne(e => e.User);
            modelBuilder.Entity<Project>().HasMany(e => e.Scenarios);

            // Scenario
            modelBuilder.Entity<Scenario>().ToTable("scenario", "public");
            modelBuilder.Entity<Scenario>().HasOne(e => e.Project).WithMany(e => e.Scenarios);
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// This is only used for during development when using the "dotnet ef" commands.
    /// Db migrations are applied in FPSim.Api.Startup.cs
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public static string DesignTimeConnectionString => "User ID=postgres;Password=rs0LE$;Host=localhost;Port=5432;Database=fpsim;Pooling=true;";

        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(DesignTimeConnectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
