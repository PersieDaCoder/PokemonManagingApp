using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetCountryById;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Countries;

public record GetCountryByIdRequest
{
    [FromRoute] public Guid Id { get; init; }
};
public class GetCountryByIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<GetCountryByIdRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Authorize]
    [Route("api/Countries/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get a country by Id",
        Tags = ["Countries"]
    )]
    public override async Task<ActionResult> HandleAsync(GetCountryByIdRequest request, CancellationToken cancellationToken = default)
    {
        Result<CountryDTO> result = await _mediator.Send(new GetCountryByIdQuery
        {
            Id = request.Id,
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Ok(result);
    }
}