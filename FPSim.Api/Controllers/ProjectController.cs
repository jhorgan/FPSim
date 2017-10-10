using System.Collections.Generic;
using FPSim.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FPSim.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Project")]
    public class ProjectController : Controller
    {
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return new[]
            {
                new Project() {Id = 1, Name="Bearings FP", Description = "This is some description about the project", ImageUrl = "/images/project-fp.png"},
                new Project() {Id = 2, Name="Terminal Planning", Description = "Green Hill Terminal 2.3", ImageUrl = "/images/project-tp.png"}
            };
        }

    }
}