using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetCountryById;
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
    [Route("api/Countries/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get a country by Id",
        Description = "Get a country by Id",
        OperationId = "Country.GetById",
        Tags = new[] { "Countries" }
    )]
    public override async Task<ActionResult> HandleAsync(GetCountryByIdRequest request, CancellationToken cancellationToken = default)
    {
        Result<CountryDTO> result = await _mediator.Send(new GetCountryByIdQuery
        {
            Id = request.Id,
        }, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }
}