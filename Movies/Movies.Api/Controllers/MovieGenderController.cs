using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Models;
using Movies.Business.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGenderController : ControllerBase
    {
        private readonly IMovieGenderBusiness _movieGenderBusiness;

        public MovieGenderController(IMovieGenderBusiness movieGenderBusiness)
        {
            _movieGenderBusiness = movieGenderBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenderMovieAsync(MovieGenderModel movieGender)
        {
            if (movieGender == null)
            {
                return BadRequest();
            }
            try
            {
                await _movieGenderBusiness.CreateGenderMovieAsync(movieGender);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGenderMovieAsync(Guid id)
        {
            await _movieGenderBusiness.DeleteGenderMovieAsync(id);
            return Ok();
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetGenderMovieByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var result = await _movieGenderBusiness.GetGenderMovieByNameAsync(name);
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenderMovieByIdAsync(Guid id)
        {
            var result = await _movieGenderBusiness.GetGenderMovieByIdAsync(id); // se obtiene eel resultado
            if (result == null) //se valida si el resultado es nulo
            {
                return NotFound(); // no fue encontrado el valor
            }
            return Ok(result);// si si devuelve Ok con el resultado
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGenderMovieByIdAsync(MovieGenderModel movieGender)
        {
            if (movieGender == null)
            {
                return BadRequest();
            }
            try
            {
                await _movieGenderBusiness.UpdateGenderMovieByIdAsync(movieGender);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}