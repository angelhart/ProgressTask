using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AsyncSubmit.Models;
using System.Net.Http;
using AsyncSubmit.Providers.Contracts;

namespace AsyncSubmit.Controllers
{
    public class HomeController : Controller
    {
        private readonly string POSTURI = "https://us-central1-randomfails.cloudfunctions.net/submitEmail";
        private readonly int TIMEOUTATTEMPTS = 10;

        private readonly ILogger<HomeController> _logger;
        private readonly IDataParser _dataParser;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger,
                              IDataParser dataParser,
                              IHttpClientFactory httpClientFactory)
        {
            this._logger = logger;
            this._dataParser = dataParser;
            this._httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(FormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();

                var request = _dataParser.Serialize(model);
                var response = await client.PostAsync(POSTURI, request);

                var i = 0;

                while (!response.IsSuccessStatusCode && ++i < TIMEOUTATTEMPTS)
                {
                    response = await client.PostAsync(POSTURI, request);
                }

                return StatusCode((int)response.StatusCode, response.Content);
            }
            else
            {
                return BadRequest("Invalid input");
            }
        }
    }
}
