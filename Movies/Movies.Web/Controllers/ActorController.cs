using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Models;
using Movies.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Web.Controllers
{
    public class ActorController : Controller
    {
        private readonly HttpClient _httpClient;

        public ActorController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var result = await _httpClient.GetAsync("Actor");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<ActorModel>>(content);
                return View(list);
            }
            return View(Enumerable.Empty<ActorModel>());
        }

        public async Task<IActionResult> Create()
        {
            var genders = await GetGendersAsync();
            var countries = await GetCountryAsync();
            var actor = new ActorViewModel();
            actor.Genders = genders.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
            actor.Nacionalities = countries.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });

            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActorModel actorModel)
        {
            var json = JsonConvert.SerializeObject(actorModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("Actor", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(actorModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _httpClient.GetAsync($"Actor/{id}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var genders = await GetGendersAsync();
                var countries = await GetCountryAsync();
                var actor = JsonConvert.DeserializeObject<ActorViewModel>(content);
                actor.Genders = genders.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
                actor.Nacionalities = countries.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
                return View(actor);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActorModel actorModel)
        {
            var json = JsonConvert.SerializeObject(actorModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync("Actor", data);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(actorModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"Actor/{id}");
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<GenderModel>> GetGendersAsync()
        {
            var result = await _httpClient.GetAsync("Gender");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var genders = JsonConvert.DeserializeObject<IEnumerable<GenderModel>>(content);
                return genders;
            }
            return Enumerable.Empty<GenderModel>();
        }

        private async Task<IEnumerable<CountryModel>> GetCountryAsync()
        {
            var result = await _httpClient.GetAsync("Country");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<IEnumerable<CountryModel>>(content);
                return countries;
            }
            return Enumerable.Empty<CountryModel>();
        }
    }
}