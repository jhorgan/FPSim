using System;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Lazy<IProjectRepository> _projectRepository;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(_context));
        }

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
