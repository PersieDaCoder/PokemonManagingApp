using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using DemoAPI.Core.DTOs.Pokemon;
using DemoAPI.UseCases.Pokemon.Queries.GetAllPokemons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Web.Endpoints.Pokemons.Get;

public class GetAllPokemonsEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult<IEnumerable<PokemonDTO>>
{
  private readonly IMediator _mediator = mediator;
  [HttpGet]
  [Route("/api/Pokemons")]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
  public override async Task<ActionResult<IEnumerable<PokemonDTO>>> HandleAsync(CancellationToken cancellationToken = default)
  {
    return Ok(await _mediator.Send(new GetAllPokemonsQuery(), cancellationToken));
  }
}