using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FPSim.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FPSim.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        private static readonly string ExperimentResultsUriFmt = "https://devio.witness.cloud/api/experiment/{0}/results";
        private static readonly Uri TokenUri = new Uri("https://devio.witness.cloud/token");

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

        [HttpGet]
        [Route("GetResults")]
        public async Task<IActionResult> GetResults()
        {
            var experimentId = "jhorgan@lanner.com-__-251894049395589-ff6952e1-a674-47a5-85f6-65641e19d96e";
            var bearerToken = await GetBearerToken();

            IActionResult result;
            try
            {
                var resultUris = new List<string>();
                var requestUri = string.Format(ExperimentResultsUriFmt, experimentId);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

                    var response = await client.GetAsync(requestUri);
                    response.EnsureSuccessStatusCode();

                    var resultJson = await response.Content.ReadAsStringAsync();
                    var results = JsonConvert.DeserializeObject<JArray>(resultJson);

                    resultUris.AddRange(results.Select(item => item.Value<string>("ModelResultsUri")));
                }
                return new ObjectResult(resultUris.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = BadRequest(e.ToString());
            }

            return result;
        }

        private static async Task<string> GetBearerToken()
        {
            const string username = "jhorgan@lanner.com";
            const string password = "VC++rule2";

            using (var client = new HttpClient())
            {
                var formData = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                };
                var content = new FormUrlEncodedContent(formData);

                var postResponse = await client.PostAsync(TokenUri, content);
                postResponse.EnsureSuccessStatusCode();


                var postResult = await postResponse.Content.ReadAsStringAsync();
                var attributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(postResult);
                var t = attributes["access_token"];

                return t;
            }
        }

    }
}