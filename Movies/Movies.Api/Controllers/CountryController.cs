﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Models;
using Movies.Business.Interfaces;
using Movies.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryBusiness _countryBusiness;
        private readonly IMapper _mapper;

        public CountryController(ICountryBusiness countryBusiness, IMapper mapper)
        {
            _countryBusiness = countryBusiness;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountryAsync(CountryModel countryModel)
        {
            if (countryModel == null)
            {
                return BadRequest();
            }
            try
            {
                var country = await _countryBusiness.CreateCountryAsync(_mapper.Map<Country>(countryModel));
                return Ok(_mapper.Map<CountryModel>(country));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCountryAsync(Guid id)
        {
            await _countryBusiness.DeleteCountryAsync(id);
            return Ok();
        }

        [HttpGet("by-name/{nameCountry}")]
        public async Task<IActionResult> GetCountriesByNameAsync(string nameCountry)
        {
            if (string.IsNullOrEmpty(nameCountry))
            {
                return BadRequest();
            }
            var result = await _countryBusiness.GetCountriesByNameAsync(nameCountry);
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result.Select(c => _mapper.Map<CountryModel>(c)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountryByIdAsync(CountryModel country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _countryBusiness.UpdateCountryByIdAsync(_mapper.Map<Country>(country));
            return Ok();
        }
    }
}