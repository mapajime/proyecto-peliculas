using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly HttpClient _httpClient;
        public CountryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieApiClient");
        }
        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetAsync("Country");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<IEnumerable<CountryModel>>(content);
                return View(countries);
            }

            return View(Enumerable.Empty<CountryModel>());
        }
    }
}
