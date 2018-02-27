using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using FPSim.Api.Model;
using FPSim.Data.Entity;
using FPSim.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPSim.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user/{userId}/project")]
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProjectController> _logger;
        private readonly IHostingEnvironment _environment;
        private readonly IMapper _mapper;

        public ProjectController(AppDbContext context, ILogger<ProjectController> logger, IHostingEnvironment environment, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
            _mapper = mapper;
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
                    var dto = _mapper.Map<Project, ProjectDto>(project);

                    result = new ObjectResult(dto);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error fetching Project {projectId}", projectId);
                result = NotFound(e.Message);
            }

            return result;
        }

        [HttpGet]
        public IActionResult GetForUser(int userId)
        {
            IActionResult result;

            _logger.LogDebug("Fetching Projects for user {userId}...", userId);
            try
            {
                using (var unitOfWork = new UnitOfWork(_context))
                {
                    var projects = unitOfWork.Projects.GetProjectsForUser(userId);
                    var dto = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(projects);

                    result = new ObjectResult(dto.ToList());
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
        public IActionResult Post([FromBody] ProjectDto projectDto)
        {
            IActionResult result;

            if (projectDto != null)
            {
                _logger.LogDebug("Creating a new Project \"{name}\"...", projectDto.Name);
                try
                {
                    var newProject = _mapper.Map<ProjectDto, Project>(projectDto);
                    newProject.DateCreated = DateTime.Now;

                    using (var unitOfWork = new UnitOfWork(_context))
                    {
                        unitOfWork.Projects.Add(newProject);
                        unitOfWork.Complete();

                        var dto = _mapper.Map<Project, ProjectDto>(newProject);
                        result = CreatedAtAction("Get", "Project", new { projectId = dto.Id }, dto);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error creating a new Project, name {name}", projectDto.Name);
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