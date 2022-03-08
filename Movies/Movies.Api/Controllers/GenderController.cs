﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.Interfaces;
using Movies.Entities;
using System;
using System.Threading.Tasks;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderBusiness _genderBusiness;

        public GenderController(IGenderBusiness genderBusiness)
        {
            _genderBusiness = genderBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenderAsync(Gender gender)
        {
            if (gender == null)
            {
                return BadRequest();
            }
            try
            {
                await _genderBusiness.CreateGenderAsync(gender);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id : Guid}")]
        public async Task<IActionResult> DeleteGenderAsync(Guid id)
        {
            await _genderBusiness.DeleteGenderAsync(id);
            return Ok();
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetGenderByNameAsync(string name)
        {
            var result = await _genderBusiness.GetGenderByNameAsync(name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGenderByIdAsync(Gender gender)
        {
            if (gender == null)
            {
                return BadRequest();
            }
            try
            {
                await _genderBusiness.UpdateGenderByIdAsync(gender);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}