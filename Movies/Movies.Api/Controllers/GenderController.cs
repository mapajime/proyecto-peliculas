using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Models;
using Movies.Business.Interfaces;
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
        public async Task<IActionResult> CreateGenderAsync(GenderModel gender)
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
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var result = await _genderBusiness.GetGenderByNameAsync(name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGenderByIdAsync(GenderModel gender)
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