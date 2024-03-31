using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetAllGyms;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Gyms;

public class GetAllGymEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Authorize]
    [Route("api/Gyms")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get all gyms",
        Tags = ["Gyms"]
    )]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        Result<IEnumerable<GymDTO>> result = await _mediator.Send(new GetAllGymsQuery(), cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Ok(result);
    }
}