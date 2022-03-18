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
    public class GenderController : Controller
    {
        private readonly HttpClient _httpClient;
        public GenderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieApiClient");
        }
        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetAsync("Gender");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var genders = JsonConvert.DeserializeObject<IEnumerable<GenderModel>>(content);
                return View(genders);
            }

            return View(Enumerable.Empty<GenderModel>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenderModel genderModel)
        {
            var json = JsonConvert.SerializeObject(genderModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("Gender", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(genderModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _httpClient.GetAsync($"Gender/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var gender = JsonConvert.DeserializeObject<GenderModel>(content);
                return View(gender);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GenderModel genderModel)
        {
            var json = JsonConvert.SerializeObject(genderModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync("Gender", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(genderModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"Gender/{id}");
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
