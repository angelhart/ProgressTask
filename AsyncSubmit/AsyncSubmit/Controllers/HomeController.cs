using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AsyncSubmit.Models;
using System.Net.Http;
using AsyncSubmit.Providers;

namespace AsyncSubmit.Controllers
{
    public class HomeController : Controller
    {
        private const string POSTURI = "https://us-central1-randomfails.cloudfunctions.net/submitEmail";

        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger,
                              IHttpClientFactory httpClientFactory)
        {
            this._logger = logger;
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

                var request = JsonProvider<FormViewModel>.Serialize(model);
                var response = await client.PostAsync(POSTURI, request);

                while (!response.IsSuccessStatusCode)
                {
                    response = await client.PostAsync(POSTURI, request);
                }

                //await Task.Delay(5000);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
