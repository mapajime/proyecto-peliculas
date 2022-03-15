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
    public class ActorController : ControllerBase
    {
        private readonly IActorBusiness _actorBusiness;
        private readonly IMapper _mapper;

        public ActorController(IActorBusiness actorBusiness, IMapper mapper)
        {
            _actorBusiness = actorBusiness;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActorAsync(ActorModel actorModel)
        {
            if (actorModel == null)
            {
                return BadRequest();
            }
            try
            {
                var actor = await _actorBusiness.CreateActorAsync(_mapper.Map<Actor>(actorModel));
                
                return Ok(_mapper.Map<ActorModel>(actor));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActorsAsync()
        {
            var result = await _actorBusiness.GetAllActorsAsync();
            return Ok(result.Select(a => _mapper.Map<ActorModel>(a)));
        }

        [HttpGet("by-last-name/{lastName}")]
        public async Task<IActionResult> GetActorsByLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return BadRequest();
            }
            var result = await _actorBusiness.GetActorByLastNameAsync(lastName);
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result.Select(a => _mapper.Map<ActorModel>(a)));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetActorById(Guid id)
        {
            var result = await _actorBusiness.GetActorByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ActorModel>(result));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteActorByIdAsync(Guid id)
        {
            await _actorBusiness.DeleteActorAsync(id);
            return Ok();
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetActorCountAsync() => Ok(await _actorBusiness.GetActorCountAsync());

        //[HttpGet]
        //public async Task<IActionResult> GetAllActorsAsync()
        //{
        //    var result = await _actorBusiness.GetAllActorsAsync();
        //    return Ok(result.Select(a => _mapper.Map<ActorModel>(a)));
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateActorByIdAsync(ActorModel actor)
        {
            if (actor == null)
            {
                return BadRequest();
            }
            try
            {
                await _actorBusiness.UpdateActorByIdAsync(_mapper.Map<Actor>(actor));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}