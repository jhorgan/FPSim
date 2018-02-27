using System.Collections.Generic;
using System.Linq;
using FPSim.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository
{
    public class ScenarioRepository : Repository<Scenario>, IScenarioRepository
    {
        public ScenarioRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Scenario> GetScenariosForProject(int projectId)
        {
            return Context.Set<Scenario>()
                .Where(scenario => scenario.ProjectId == projectId)
                .Select(scenario => scenario);

        }
    }
}