using MediatR;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Api.Models;
using RickAndMorty.Core.Services.Commands;
using RickAndMorty.Core.Services.Queries;

namespace RickAndMorty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CharacterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get the list of all characters
        /// </summary>
        /// <param name="originId">Origin Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] long? originId = null, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetCharactersQuery(originId), cancellationToken);

            Response.Headers.Add("from-database", response.FromDatabase.ToString());

            return Ok(response.Result);
        }

        /// <summary>
        /// Add a character
        /// </summary>
        /// <param name="input">Character input</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CharacterInputModel input, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new AddCharacterCommand(
                input.Name,
                input.Species,
                input.Type,
                input.Gender,
                input.ImageUrl,
                input.OriginId,
                input.LocationId), cancellationToken);

            return response.Succeeded ? Ok(response.Result) : BadRequest(response.Errors);
        }
    }
}
