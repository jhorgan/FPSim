using System;

namespace FPSim.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepository Applications { get; }
        IUserRepository Users { get; }
        IProjectRepository Projects { get; }
        IScenarioRepository Scenarios { get; }

        int Complete();
    }
}
