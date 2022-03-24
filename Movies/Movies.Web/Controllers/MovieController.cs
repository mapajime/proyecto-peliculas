using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly HttpClient _httpClient;
        public MovieController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieApiClient");
        }
        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetAsync("Movie");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<MovieModel>>(content);
                return View(list);
            }
            return View(Enumerable.Empty<MovieModel>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieModel movieModel)
        {
            var json = JsonConvert.SerializeObject(movieModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("Movie", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(movieModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _httpClient.GetAsync($"Movie/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var movie = JsonConvert.DeserializeObject<MovieModel>(content);
                return View(movie);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieModel movieModel)
        {
            var json = JsonConvert.SerializeObject(movieModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync("Movie",data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(movieModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _httpClient.GetAsync($"Movie/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var movie = JsonConvert.DeserializeObject<MovieModel>(content);
                return View(movie);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MovieModel movieModel)
        {
            var resul = await _httpClient.DeleteAsync($"Movie/{movieModel.Id}");
            if (resul.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(movieModel);
        }
    }
}
