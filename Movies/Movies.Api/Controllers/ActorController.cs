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
    public class ActorController : ControllerBase
    {
        private readonly IActorBusiness _actorBusiness;

        public ActorController(IActorBusiness actorBusiness)
        {
            _actorBusiness = actorBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActorAsync(Actor actor)
        {
            if (actor == null)
            {
                return BadRequest();
            }
            try
            {
                await _actorBusiness.CreateActorAsync(actor);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActorsAsync() => Ok(await _actorBusiness.GetAllActorsAsync());

        [HttpGet("by-last-name/{lastName}")]
        public async Task<IActionResult> GetActorsByLastName(string lastName)
        {
            var result = await _actorBusiness.GetActorByLastNameAsync(lastName);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetActorById(Guid id)
        {
            var result = await _actorBusiness.GetActorByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteActorByIdAsync(Guid id)
        {
            await _actorBusiness.DeleteActorAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetActorCountAsync() => Ok(await _actorBusiness.GetActorCountAsync());

        [HttpGet]
        public async Task<IActionResult> GetAllActorsAsync() => Ok(await _actorBusiness.GetAllActorsAsync());

        [HttpPut]
        public async Task<IActionResult> UpdateActorByIdAsync(Actor actor)
        {
            if (actor == null)
            {
                return BadRequest();
            }
            try
            {
                await _actorBusiness.UpdateActorByIdAsync(actor);
                return Ok();
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
    }
}
