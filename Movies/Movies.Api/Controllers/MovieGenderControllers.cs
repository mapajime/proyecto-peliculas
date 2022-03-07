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
    public class MovieGenderControllers : ControllerBase
    {
        private readonly IMovieGenderBusiness _movieGenderBusiness;

        public MovieGenderControllers(IMovieGenderBusiness movieGenderBusiness)
        {
            _movieGenderBusiness = movieGenderBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenderMovieAsync(MovieGender movieGender)
        {
            if (movieGender == null)
            {
                return BadRequest();
            }
            await _movieGenderBusiness.CreateGenderMovieAsync(movieGender);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGenderMovieAsync(Guid id)
        {
            await _movieGenderBusiness.DeleteGenderMovieAsync(id);
            return Ok();
        }

        [HttpGet ("by-name/{name}")]
        public async Task<IActionResult> GetGenderMovieByNameAsync(string name)
        {
            await _movieGenderBusiness.GetGenderMovieByNameAsync(name);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGenderMovieByIdAsync(MovieGender movieGender)
        {
            if (movieGender == null)
            {
                return NotFound();
            }
            await _movieGenderBusiness.UpdateGenderMovieByIdAsync(movieGender);
            return Ok();
        }
    }
}
