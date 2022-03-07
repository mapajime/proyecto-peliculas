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
    public class LanguageControllers : ControllerBase
    {
        private readonly ILanguageBusiness _languageBusiness;

        public LanguageControllers(ILanguageBusiness languageBusiness)
        {
            _languageBusiness = languageBusiness;
        }
        [HttpPost]
        public async Task<IActionResult> CreateLanguageAsync(Language language)
        {
            if (string.IsNullOrEmpty(language.Name))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _languageBusiness.CreateLanguageAsync(language);
            return Ok();
        }

        [HttpDelete ("{id : Guid}")]
        public async Task<IActionResult> DeleteLanguageAsync(Guid id)
        {
            await _languageBusiness.DeleteLanguageAsync(id);
            return Ok();
        }

        [HttpGet ("by-name/{name}")]
        public async Task<IActionResult> GetLanguagesByNameAsync(string name)
        {
            var result = await _languageBusiness.GetLanguagesByNameAsync(name);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLanguageByIdAsync(Language language)
        {
            if (language == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            await _languageBusiness.UpdateLanguageByIdAsync(language);
            return Ok();
        }
    }
}
