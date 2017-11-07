using System;
using System.IO;
using System.Linq;
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
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(AppDbContext context, ILogger<ProjectController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int userId)
        {
            IActionResult result;

            _logger.LogDebug("Fetching Project for user {userId}...", userId);
            try
            {
                using (var unitOfWork = new UnitOfWork(_context))
                {
                    var projects = unitOfWork.Projects.GetProjectsForUser(userId);
                    result = new ObjectResult(projects.ToList());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error fetching Projects for user {userId}", userId);
                result = NotFound(e.Message);
            }

            return result;
        }

        [HttpGet("{projectId}/image")]
        public IActionResult GetProjectImage(int projectId)
        {
            IActionResult result;

            _logger.LogDebug("Fetching Project image for project {projectId}...", projectId);
            try
            {
                using (var unitOfWork = new UnitOfWork(_context))
                {
                    var imageByteArray = unitOfWork.Projects.GetProjectImage(projectId);
                    var stream = new MemoryStream(imageByteArray);

                    // Note: stream will be disposed by FileStreamResult 
                    result = new FileStreamResult(stream, "image/png");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error fetching Project image for project {projectId}", projectId);
                result = NotFound(e.Message);
            }

            return result;
        }
    }
}