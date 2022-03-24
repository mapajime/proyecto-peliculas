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

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryModel countryModel)
        {
            var json = JsonConvert.SerializeObject(countryModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("Country", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(countryModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _httpClient.GetAsync($"Country/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var country = JsonConvert.DeserializeObject<CountryModel>(content);
                return View(country);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryModel countryModel)
        {
            var json = JsonConvert.SerializeObject(countryModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync("Country", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(countryModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"Country/{id}");
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}