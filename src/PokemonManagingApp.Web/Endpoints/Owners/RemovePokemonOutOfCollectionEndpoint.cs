using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.RemovePokemonOutOfCollection;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record RemovePokemonOutOfCollectionRequest
{
    [Required(ErrorMessage = "Pokemon Id is required")]
    [FromRoute] public Guid PokemonId { get; set; }
    [Required(ErrorMessage = "Owner Id is required")]
    [FromRoute] public Guid OwnerId { get; set; }
}
public class RemovePokemonOutOfCollectionEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<RemovePokemonOutOfCollectionRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete]
    [Route("api/Owners/{OwnerId:guid}/Pokemons/{PokemonId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [SwaggerOperation(
          Summary = "Remove a Pokemon from the collection of an owner",
          Tags = ["Owners"]
      )]
    public override async Task<ActionResult> HandleAsync(RemovePokemonOutOfCollectionRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new RemovePokemonOutOfCollectionCommand
        {
            OwnerId = request.OwnerId,
            PokemonId = request.PokemonId
        });
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.Contains("not found", StringComparison.OrdinalIgnoreCase))) return NotFound(result);
            return BadRequest(result);
        }
        return NoContent();
    }
}