using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Movies.Web.Controllers
{
    public class MovieGenderController : Controller
    {
        private readonly HttpClient _httpClient;

        public MovieGenderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieApiClient");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}