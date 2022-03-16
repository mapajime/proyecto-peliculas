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
    public class GenderController : ControllerBase
    {
        private readonly IGenderBusiness _genderBusiness;
        private readonly IMapper _mapper;

        public GenderController(IGenderBusiness genderBusiness, IMapper mapper)
        {
            _genderBusiness = genderBusiness;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenderAsync(GenderModel genderModel)
        {
            if (genderModel == null)
            {
                return BadRequest();
            }
            try
            {
                var gender = await _genderBusiness.CreateGenderAsync(_mapper.Map<Gender>(genderModel));
                return Ok(_mapper.Map<GenderModel>(gender));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
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
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result.Select(g => _mapper.Map<GenderModel>(g)));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetGenderByIdAsync(Guid id)
        {
            var result = await _genderBusiness.GetGenderByIdAsync(id);
            return Ok(_mapper.Map<GenderModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGendersAsync()
        {
            var result = await _genderBusiness.GetAllGendersAsync();
            return Ok(result.Select(g => _mapper.Map<GenderModel>(g)));
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
                await _genderBusiness.UpdateGenderByIdAsync(_mapper.Map<Gender>(gender));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}