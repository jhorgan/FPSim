using System;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Lazy<IApplicationRepository> _applicationRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IProjectRepository> _projectRepository;
        
        public UnitOfWork(DbContext context)
        {
            _context = context;            

            _applicationRepository = new Lazy<IApplicationRepository>(() => new ApplicationRepository(_context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(_context));
        }

        public IApplicationRepository Applications => _applicationRepository.Value;
        public IUserRepository Users => _userRepository.Value;
        public IProjectRepository Projects => _projectRepository.Value;        

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
