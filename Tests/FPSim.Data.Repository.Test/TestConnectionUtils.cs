using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository.Test
{
    internal class TestConnectionUtils
    {
        public static string ConnectionString => AppDbContextFactory.DesignTimeConnectionString;

        internal static AppDbContext CreateDbContext()
        {
            var contectFactory = new AppDbContextFactory();
            return contectFactory.CreateDbContext(new string[] { });
        }        
    }
}
