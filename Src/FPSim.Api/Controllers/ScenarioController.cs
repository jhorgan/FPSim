using System;
using System.Collections.Generic;
using AutoMapper;
using FPSim.Api.Model;
using FPSim.Data.Entity;
using FPSim.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPSim.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user/{userId}/project/{projectId}/scenario")]
    public class ScenarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ScenarioController> _logger;
        private readonly IMapper _mapper;

        public ScenarioController(AppDbContext context, ILogger<ScenarioController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{scenarioId}")]
        public IActionResult Get(int scenarioId)
        {
            return new ObjectResult(new[] { "Scenario", scenarioId.ToString() });
        }

        [HttpGet]
        public IActionResult GetForProject(int projectId)
        {
            IActionResult result;

            _logger.LogDebug("Fetching Scenarios for project {projectId}...", projectId);
            try
            {
                using (var unitOfWork = new UnitOfWork(_context))
                {
                    var scenarios = unitOfWork.Scenarios.GetScenariosForProject(projectId);
                    var dto = _mapper.Map<IEnumerable<Scenario>, IEnumerable<ScenarioDto>>(scenarios);

                    result = new ObjectResult(dto);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error fetching Scenarios for project {projectId}", projectId);
                result = NotFound(e.Message);
            }

            return result;
        }
    }
}