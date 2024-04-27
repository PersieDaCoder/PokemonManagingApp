using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllOwners;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public class GetAllOwnersEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult
{
  private readonly IMediator _mediator = mediator;

  [HttpGet]
  [Authorize]
  [Route("/api/owners")]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
  [SwaggerOperation(
      Summary = "Get all Owners",
      Tags = ["Owners"]
  )]
  public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
  {
    Result<IEnumerable<OwnerDTO>> result = await _mediator.Send(new GetAllOwnersQuery(), cancellationToken);
    if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
    return Ok(result);
  }
}