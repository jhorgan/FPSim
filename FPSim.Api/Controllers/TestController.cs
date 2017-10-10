using System;
using System.Linq;
using FPSim.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace FPSim.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        private readonly WitnessCloudContext _context;

        public TestController(WitnessCloudContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string Get()
        {
            return "value";
        }

        [HttpGet]
        [Route("GetVersion")]
        public IActionResult GetVersion()
        {
            IActionResult result;
            try
            {
                result = new ObjectResult(_context.Version.FirstOrDefault());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = BadRequest(e.ToString());
            }

            return result;
        }
        [HttpGet]
        [Route("GetProjects")]
        public IActionResult GetProjects()
        {
            IActionResult result;
            try
            {
                using (var context = new FPSimAppContext())
                {
                    result = new ObjectResult(context.Projects.ToList());
                }                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = BadRequest(e.ToString());
            }

            return result;
        }

    }
}