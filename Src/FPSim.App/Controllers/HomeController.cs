using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FPSim.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var indexHtml = Path.Combine(_hostingEnvironment.ContentRootPath, "ClientApp", "index.html");
            return new PhysicalFileResult(indexHtml, "text/html");
        }
    }
}
