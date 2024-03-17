using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Reviews.Queries.GetReviewById;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviews;

public record GetReviewByIdRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; init; }
}
public class GetReviewByIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<GetReviewByIdRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;
    [HttpGet]
    [Route("/api/Reviews/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get Review by Id",
        Tags = ["Reviews"]
    )]
    public override async Task<ActionResult> HandleAsync(GetReviewByIdRequest request, CancellationToken cancellationToken = default)
    {
        Result<ReviewDTO> result = await _mediator.Send(new GetReviewByIdQuery
        {
            Id = request.Id
        }, cancellationToken);
        if (!result.IsSuccess) return NotFound(result);
        return Ok(result);
    }
}