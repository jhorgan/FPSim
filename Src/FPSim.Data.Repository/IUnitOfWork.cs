using System;

namespace FPSim.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }

        int Complete();
    }
}
