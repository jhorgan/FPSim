using System;
using System.IO;
using System.Linq;
using FPSim.Data.Entity;
using FPSim.Data.Repository;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _environment;

        public ProjectController(AppDbContext context, ILogger<ProjectController> logger, IHostingEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        [HttpGet("{projectId}")]
        public IActionResult Get(int projectId)
        {
            IActionResult result;

            _logger.LogDebug("Fetching Project {projectId}...", projectId);
            try
            {
                using (var unitOfWork = new UnitOfWork(_context))
                {
                    var project = unitOfWork.Projects.Get(projectId);
                    result = new ObjectResult(project);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error fetching Project {projectId}", projectId);
                result = NotFound(e.Message);
            }

            return result;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetForUser(int userId)
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
                    var imageByteArray = unitOfWork.Projects.GetProjectImage(projectId) ?? GetDefaultProjectImage();
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

        private byte[] GetDefaultProjectImage()
        {
            return System.IO.File.ReadAllBytes(Path.Combine(_environment.WebRootPath, "images", "default-project.png"));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            IActionResult result;

            if (project != null)
            {
                _logger.LogDebug("Creating a new Project \"{name}\"...", project.Name);
                try
                {
                    var newProject = new Project()
                    {
                        Name = project.Name,
                        Description = project.Description,
                        UserId = project.UserId,
                        ApplicationId = project.ApplicationId,
                        DateModified = DateTime.Now,
                        DateCreated = DateTime.Now
                    };
                    using (var unitOfWork = new UnitOfWork(_context))
                    {
                        unitOfWork.Projects.Add(newProject);
                        unitOfWork.Complete();

                        result = CreatedAtAction("Get", "Project", new {projectId = newProject.Id}, project);                        
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error creating a new Project, name {name}", project.Name);
                    result = BadRequest(e.Message);
                }
            }
            else
            {
                result = BadRequest();
            }
            return result;
        }
    }
}