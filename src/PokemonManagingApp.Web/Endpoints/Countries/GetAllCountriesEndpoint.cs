using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetAllCountries;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Countries;

public class GetAllCountriesEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Route("api/Countries")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get all countries",
        Tags = ["Countries"]
    )]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        Result<IEnumerable<CountryDTO>> result = await _mediator.Send(new GetAllCountriesQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}