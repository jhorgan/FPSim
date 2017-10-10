using Microsoft.EntityFrameworkCore;

namespace FPSim.Api.Data
{
    public class WitnessCloudContext : DbContext
    {
        public WitnessCloudContext(DbContextOptions options) : base(options)
        {            
        }

        public DbSet<DbVersion> Version { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbVersion>().ToTable("tblVersion");
            modelBuilder.Entity<DbVersion>().Property(t => t.Id).HasColumnName("Version_ID");
            modelBuilder.Entity<DbVersion>().Property(t => t.Number).HasColumnName("Version_Number");
            modelBuilder.Entity<DbVersion>().Property(t => t.Description).HasColumnName("Version_Description");
        }
    }

    public class DbVersion
    {
        public int Id { get; set; }
        public double Number { get; set; }
        public string Description { get; set; }
    }
}