using System.Collections.Generic;
using System.Linq;
using FPSim.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Project> GetProjectsForUser(int userId)
        {
            return Context.Set<Project>()
                .Where(project => project.UserId == userId);
        }

        public IEnumerable<Project> GetProjectsAndReleatedScenariosForUser(int userId)
        {
            return Context.Set<Project>()
                .Where(project => project.UserId == userId && !project.IsArchived)
                .Include(project => project.Scenarios);
        }
    }
}
