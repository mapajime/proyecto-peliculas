using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Movies.Web.Utilities;

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

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieGenderModel movieGenderModel)
        {
            var result = await _httpClient.PostAsync("MovieGender", movieGenderModel.CreateContentFromObject());
            if (result.IsSuccessStatusCode)
            {
                //var content = await result.Content.ReadAsStringAsync();
                //var movieGender = JsonConvert.DeserializeObject<MovieGenderModel>(content);
                //return View(movieGender);
                return RedirectToAction("Index");
            }
            return View(movieGenderModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _httpClient.GetAsync($"MovieGender/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var movieGenderModel = JsonConvert.DeserializeObject<MovieGenderModel>(content);
                return View(movieGenderModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MovieGenderModel movieGenderModel)
        {
            var result = await _httpClient.PutAsync("MovieGender", movieGenderModel.CreateContentFromObject());
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(movieGenderModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _httpClient.GetAsync($"MovieGender/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var movieGenderModel = JsonConvert.DeserializeObject<MovieGenderModel>(content);
                return View(movieGenderModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(MovieGenderModel movieGenderModel)
        {
            var resul = await _httpClient.DeleteAsync($"MovieGender/{movieGenderModel.Id}");
            if (resul.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(movieGenderModel);
        }

    }
}