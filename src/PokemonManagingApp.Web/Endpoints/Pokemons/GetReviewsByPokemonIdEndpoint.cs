using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.UseCase_Pokemons.Queries.GetReviewsByPokemonId;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;

public record GetReviewsByPokemonIdRequest
{
  [Required(ErrorMessage = "Pokemon Id is required")]
  [FromRoute] public Guid PokemonId { get; init; }
}
public class GetReviewsByPokemonId(IMediator mediator) : EndpointBaseAsync.WithRequest<GetReviewsByPokemonIdRequest>.WithActionResult
{
  private readonly IMediator _mediator = mediator;
  [HttpGet]
  [Authorize]
  [Route("api/Pokemons/{PokemonId}/Reviews")]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
  [SwaggerOperation(
    Summary = "Get Reviews by Pokemon Id",
    Tags = ["Pokemons"]
  )]
  public override async Task<ActionResult> HandleAsync(GetReviewsByPokemonIdRequest request, CancellationToken cancellationToken = default)
  {
    Result<IEnumerable<ReviewDTO>> result = await _mediator.Send(new GetReviewsByPokemonIdQuery
    {
      PokemonId = request.PokemonId
    }, cancellationToken);
    if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
    return Ok(result);
  }
}