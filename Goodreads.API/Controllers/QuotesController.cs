using Goodreads.API.Common;
using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.Quotes.Commands.CreateQuote;
using Goodreads.Application.Quotes.Queries.GetQuoteById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Goodreads.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController(IMediator mediator, IUserContext userContext) : ControllerBase
    {
        [HttpGet("{id}")]
        [EndpointSummary("Get quote by ID")]
        [ProducesResponseType(typeof(ApiResponse<QuoteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuoteById(string id)
        {
            var result = await mediator.Send(new GetQuoteByIdQuery(id));
            return result.Match(
                quote => Ok(ApiResponse<QuoteDto>.Success(quote)),
                failure => CustomResults.Problem(failure));
        }
        [HttpPost]
        [Authorize]
        [EndpointSummary("Create a new quote")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteCommand command)
        {
            var result = await mediator.Send(command);
            return result.Match(
            id => CreatedAtAction(nameof(GetQuoteById), new { id }, ApiResponse.Success("Quote created successfully")),
            failure => CustomResults.Problem(failure));
        }
    }
}
