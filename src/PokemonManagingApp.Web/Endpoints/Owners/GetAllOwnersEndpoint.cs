using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllOwners;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public class GetAllOwnersEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult
{
  private readonly IMediator _mediator = mediator;

  [HttpGet]
  [Route("/api/Owners")]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
  [SwaggerOperation(
      Summary = "Get all Owners",
      Tags = ["Owners"]
  )]
  public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
  {
    Result<IEnumerable<OwnerDTO>> result = await _mediator.Send(new GetAllOwnersQuery(), cancellationToken);
    if (!result.IsSuccess) return NotFound(result);
    return Ok(result);
  }
}