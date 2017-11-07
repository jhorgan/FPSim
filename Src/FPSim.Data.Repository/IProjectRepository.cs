using System.Collections.Generic;
using FPSim.Data.Entity;

namespace FPSim.Data.Repository
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetProjectsForUser(int userId);
        IEnumerable<Project> GetProjectsAndReleatedScenariosForUser(int userId);
        byte[] GetProjectImage(int projectId);
    }
}