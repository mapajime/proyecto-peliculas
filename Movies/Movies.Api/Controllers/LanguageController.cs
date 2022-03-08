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
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageBusiness _languageBusiness;

        public LanguageController(ILanguageBusiness languageBusiness)
        {
            _languageBusiness = languageBusiness;
        }
        [HttpPost]
        public async Task<IActionResult> CreateLanguageAsync(Language language)
        {
            if (language == null)
            {
                return BadRequest();
            }
            try
            {
                await _languageBusiness.CreateLanguageAsync(language);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id : Guid}")]
        public async Task<IActionResult> DeleteLanguageAsync(Guid id)
        {
            await _languageBusiness.DeleteLanguageAsync(id);
            return Ok();
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetLanguagesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var result = await _languageBusiness.GetLanguagesByNameAsync(name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLanguageByIdAsync(Language language)
        {
            if (language == null)
            {
                return BadRequest();
            }
            try
            {
                await _languageBusiness.UpdateLanguageByIdAsync(language);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
