using FPSim.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}