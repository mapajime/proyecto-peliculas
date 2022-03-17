using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Web.Controllers
{
    public class MovieGenderController : Controller
    {
        private readonly HttpClient _httpClient;

        public MovieGenderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetAsync("MovieGender");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<MovieGenderModel>>(content);
                return View(list);
            }
            return View(Enumerable.Empty<MovieGenderModel>());
        }
    }
}