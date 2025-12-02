using Goodreads.API.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.Shelves.Commands.CreateShelf;
using Goodreads.Application.Shelves.Commands.DeleteShelf;
using Goodreads.Application.Shelves.Commands.UpdateShelf;
using Goodreads.Application.Shelves.Queries.GetShelfById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Goodreads.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController(IMediator mediator) : ControllerBase
    {


        [HttpGet("{id}")]
        [EndpointSummary("Get shelf by ID")]
        [ProducesResponseType(typeof(ApiResponse<ShelfDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShelfById(string id)
        {
            var result = await mediator.Send(new GetShelfByIdQuery(id));
            return result.Match(
                shelf => Ok(ApiResponse<ShelfDto>.Success(shelf)),
                failure => CustomResults.Problem(failure));
        }

        [HttpPost]
        [Authorize]
        [EndpointSummary("Create a new shelf")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShelf([FromBody] CreateShelfCommand command)
        {
            var result = await mediator.Send(command);
            return result.Match(
                id => CreatedAtAction(nameof(GetShelfById), new { id }, ApiResponse.Success("Shelf created successfully")),
                failure => CustomResults.Problem(failure));
        }
        [HttpPut]
        [Authorize]
        [EndpointSummary("Update a shelf")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateShelf([FromBody] UpdateShelfCommand command)
        {
            var result = await mediator.Send(command);
            return result.Match(
                () => NoContent(),
                failure => CustomResults.Problem(failure));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [EndpointSummary("Delete a shelf")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShelf(string id)
        {
            var result = await mediator.Send(new DeleteShelfCommand(id));
            return result.Match(
                () => NoContent(),
                failure => CustomResults.Problem(failure));
        }

    }
}
