using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Business.Interfaces;
using Movies.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieBusiness _movieBusiness;
        private readonly IMapper _mapper;

        public MovieController(IMovieBusiness movieBusiness, IMapper mapper)
        {
            _movieBusiness = movieBusiness;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync(MovieModel movieModel)
        {
            if (movieModel == null)
            {
                return BadRequest();
            }
            try
            {
                var movie = await _movieBusiness.CreateMovieAsync(_mapper.Map<Movie>(movieModel));
                return Ok(_mapper.Map<MovieModel>(movie));
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
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var result = await _movieBusiness.GetAllMoviesAsync();
            return Ok(result.Select(m => _mapper.Map<MovieModel>(m)));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetMovieByIdAsync(Guid id)
        {
            var result = await _movieBusiness.GetMovieByIdAsync(id);
            return Ok(_mapper.Map<MovieModel>(result));
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetMoviesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var result = await _movieBusiness.GetMoviesByNameAsync(name);
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result.Select(m => _mapper.Map<MovieModel>(m)));
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
                await _movieBusiness.UpdateMovieAsync(_mapper.Map<Movie>(movie));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}