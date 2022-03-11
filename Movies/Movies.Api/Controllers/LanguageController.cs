using AutoMapper;
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
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageBusiness _languageBusiness;
        private readonly IMapper _mapper;

        public LanguageController(ILanguageBusiness languageBusiness, IMapper mapper)
        {
            _languageBusiness = languageBusiness;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguageAsync(LanguageModel language)
        {
            if (language == null)
            {
                return BadRequest();
            }
            try
            {
                await _languageBusiness.CreateLanguageAsync(_mapper.Map<Language>(language));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
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
            return Ok(result.Select(l => _mapper.Map<LanguageModel>(l)));
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguagesByIdAsync(Guid id)
        {
            var result = await _languageBusiness.GetLanguageByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LanguageModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLanguagesAsync()
        {
            var result = await _languageBusiness.GetAllLanguagesAsync();
            return Ok(result.Select(l => _mapper.Map<LanguageModel>(l)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLanguageByIdAsync(LanguageModel language)
        {
            if (language == null)
            {
                return BadRequest();
            }
            try
            {
                await _languageBusiness.UpdateLanguageByIdAsync(_mapper.Map<Language>(language));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}