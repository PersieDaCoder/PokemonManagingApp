using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Pokemons.Queries.GetPokemonById;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;

public record GetPokemonByIdRequest
{
  [Required(ErrorMessage = "Pokemon Id is required")]
  [FromRoute] public Guid Id { get; init; }
}
public class GetPokemonByIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<GetPokemonByIdRequest>.WithActionResult
{
  private readonly IMediator _mediator = mediator;
  [HttpGet]
  [Route("api/Pokemons/{Id}")]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
  [SwaggerOperation(
    Summary = "Get a Pokemon by Id",
    Tags = ["Pokemons"]
  )]
  public override async Task<ActionResult> HandleAsync(GetPokemonByIdRequest request, CancellationToken cancellationToken = default)
  {
    Result<PokemonDTO> result = await _mediator.Send(new GetPokemonByIdQuery
    {
      Id = request.Id,
    });
    if (!result.IsSuccess) return NotFound(result);
    return Ok(result);
  }
}