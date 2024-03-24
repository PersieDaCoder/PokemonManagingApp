using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.UseCase_Pokemons;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;
public class GetAllPokemonsEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult<IEnumerable<PokemonDTO>>
{
  private readonly IMediator _mediator = mediator;
  [HttpGet]
  [Authorize]
  [Route("/api/Pokemons")]
  [SwaggerOperation(
        Summary = "Get All Pokemons",
        Tags = ["Pokemons"]
  )]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
  public override async Task<ActionResult<IEnumerable<PokemonDTO>>> HandleAsync(CancellationToken cancellationToken = default)
  {
    Result<IEnumerable<PokemonDTO>> result = await _mediator.Send(new GetAllPokemonsQuery(), cancellationToken);
    if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
    return Ok(result);
  }
}