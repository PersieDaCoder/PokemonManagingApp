using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetAllReviews;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviews;
public class GetAllReviewsEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult
{
    private readonly IMediator _mediator = mediator;
    [HttpGet]
    [Route("/api/Reviews")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get all Reviews",
        Tags = ["Reviews"]
    )]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        Result<IEnumerable<ReviewDTO>> result = await _mediator.Send(new GetAllReviewsQuery(), cancellationToken);
        if (!result.IsSuccess) return NotFound(result);
        return Ok(result);
    }
}