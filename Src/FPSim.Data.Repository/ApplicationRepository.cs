using System.Linq;
using FPSim.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(DbContext context) : base(context)
        {
        }

        public Application GetApplicationAndReleatedProjects(int applicationId)
        {
            return Context.Set<Application>()
                .Where(application => application.Id == applicationId && !application.IsArchived)
                .Include(application => application.Projects)
                .FirstOrDefault();
        }
    }
}