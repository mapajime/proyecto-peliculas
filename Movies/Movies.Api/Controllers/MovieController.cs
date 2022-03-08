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
    public class MovieController : ControllerBase
    {
        private readonly IMovieBusiness _movieBusiness;

        public MovieController(IMovieBusiness movieBusiness)
        {
            _movieBusiness = movieBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync(MovieModel movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }
            try
            {
                await _movieBusiness.CreateMovieAsync(movie);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteMovieAsync(Guid id)
        {
            await _movieBusiness.DeleteMovieAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync() => Ok(await _movieBusiness.GetAllMoviesAsync());

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetMovieByIdAsync(Guid id) => Ok(await _movieBusiness.GetMovieByIdAsync(id));

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetMoviesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var result = await _movieBusiness.GetMoviesByNameAsync(name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetNumberOfMoviesAsync() => Ok(await _movieBusiness.GetNumberOfMoviesAsync());

        [HttpPut]
        public async Task<IActionResult> UpdateMovieAsync(MovieModel movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }
            try
            {
                await _movieBusiness.UpdateMovieAsync(movie);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}