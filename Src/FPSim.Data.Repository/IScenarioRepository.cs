using System.Collections.Generic;
using FPSim.Data.Entity;

namespace FPSim.Data.Repository
{
    public interface IScenarioRepository : IRepository<Scenario>
    {
        IEnumerable<Scenario> GetScenariosForProject(int projectId);
    }
}