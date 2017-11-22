using FPSim.Data.Entity;

namespace FPSim.Data.Repository
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Application GetApplicationAndReleatedProjects(int applicationId);
    }
}