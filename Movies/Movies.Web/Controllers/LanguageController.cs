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
    public class LanguageController : Controller
    {
        private HttpClient _httpClient;

        public LanguageController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetAsync("Language");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var language = JsonConvert.DeserializeObject<IEnumerable<LanguageModel>>(content);
                return View(language);
            }

            return View(Enumerable.Empty<LanguageModel>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LanguageModel languageModel)
        {
            var json = JsonConvert.SerializeObject(languageModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("Language", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(languageModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _httpClient.GetAsync($"Language/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var language = JsonConvert.DeserializeObject<LanguageModel>(content);
                return View(language);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LanguageModel languageModel)
        {
            var json = JsonConvert.SerializeObject(languageModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync("Language", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(languageModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"Language/{id}");
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}