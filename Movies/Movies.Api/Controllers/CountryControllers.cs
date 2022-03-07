using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;
using Movies.Entities;
using System;
using System.Threading.Tasks;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryControllers : ControllerBase
    {
        private readonly ICountryBusiness _countryBusiness;

        public CountryControllers(ICountryBusiness countryBusiness)
        {
            _countryBusiness = countryBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountryAsync(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _countryBusiness.CreateCountryAsync(country);
            return Ok();
        }

        [HttpDelete ("{id : Guid}")]
        public async Task<IActionResult> DeleteCountryAsync(Guid id)
        {
            await _countryBusiness.DeleteCountryAsync(id);
            return Ok();
        }

        [HttpGet ("by-nameCountry/{nameCountry}")]
        public async Task<IActionResult> GetCountriesByNameAsync(string nameCountry)
        {
            var result = await _countryBusiness.GetCountriesByNameAsync(nameCountry);
            if (nameCountry == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountryByIdAsync(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _countryBusiness.UpdateCountryByIdAsync(country);
            return Ok();
        }
    }
}
