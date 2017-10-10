using FPSim.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Api.Data
{
    public class FPSimAppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=u67iy3frnbgct1hd;Password=vcvp26y7qozn21blp2u35dm4z;Host=db-13ad83c6-5921-4d49-bedc-42af4d67f531.c7uxaqxgfov3.us-west-2.rds.amazonaws.com;Port=5432;Database=postgres;Pooling=true;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project", "public");
        }
    }
}