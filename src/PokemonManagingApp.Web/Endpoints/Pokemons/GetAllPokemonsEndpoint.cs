using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.UseCase_Pokemons;
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
    return Ok(await _mediator.Send(new GetAllPokemonsQuery(), cancellationToken));
  }
}