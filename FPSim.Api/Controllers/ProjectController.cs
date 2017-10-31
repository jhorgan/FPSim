using System;
using FPSim.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPSim.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Project")]
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public ProjectController(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int userId)
        {
            IActionResult result;

            _logger.LogDebug("Fetching Projects and related Scenarios for user {userId}...", userId);
            try
            {
                using (var unitOfWork = new UnitOfWork(_context))
                {
                    var projects = unitOfWork.Projects.GetProjectsAndReleatedScenariosForUser(userId);
                    result = new ObjectResult(projects);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error fetching Projects and related Scenarios for user {userId}", userId);
                result = new NotFoundObjectResult(e.Message);
            }

            return result;
        }
    }
}