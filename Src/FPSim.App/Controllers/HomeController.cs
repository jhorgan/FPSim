using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FPSim.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHostingEnvironment hostingEnvironment, IConfiguration configuration, ILogger<HomeController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {

            var apiUrl = _configuration.GetValue<string>("FP_SIM_API_URL");
            _logger.LogInformation($"Api Url: {apiUrl}");

            var defaultUserId = _configuration.GetValue<string>("FP-SIM_USERID");
            _logger.LogInformation($"User Id: {defaultUserId}");

            var defaultApplicationId = _configuration.GetValue<string>("FP-SIM_APPLICATIONID");
            _logger.LogInformation($"Application Id: {defaultApplicationId}");

            // Prevent values being null as Append will throw an exception
            Response.Cookies.Append("FP-SIM_URL", apiUrl ?? "");
            Response.Cookies.Append("FP-SIM_USERID", defaultUserId ?? "1");
            Response.Cookies.Append("FP-SIM_APPLICATIONID", defaultApplicationId ?? "1");

            var indexHtml = Path.Combine(_hostingEnvironment.ContentRootPath, "ClientApp", "index.html");
            _logger.LogInformation($"Index.html path: {indexHtml}");

            return new PhysicalFileResult(indexHtml, "text/html");
        }
    }
}
