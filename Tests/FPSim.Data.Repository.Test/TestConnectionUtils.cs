using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository.Test
{
    internal class TestConnectionUtils
    {
        internal static string ConnectionString => "User ID=postgres;Password=rs0LE$;Host=localhost;Port=5432;Database=fpsim;Pooling=true;";

        internal static AppDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(ConnectionString);

            var context = new AppDbContext(builder.Options);
            context.Database.Migrate();
            
            return context;
        }
    }
}
