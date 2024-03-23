using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.AddPokemonToCollection;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record AddPokemonToCollectionRequest
{
    [Required(ErrorMessage = "Owner Id is required")]
    [FromRoute] public string OwnerId { get; set; } = null!;
    [Required(ErrorMessage = "Pokemon Id is required")]
    [FromRoute] public Guid PokemonId { get; set; }
};
public class AddPokemonToCollectionEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<AddPokemonToCollectionRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Route("api/Owners/{OwnerId:guid}/Pokemons/{PokemonId:guid}")]
    [SwaggerOperation(
          Summary = "Add a Pokemon to the collection of an owner",
          Tags = ["Owners"]
      )]
    public override async Task<ActionResult> HandleAsync(AddPokemonToCollectionRequest request, CancellationToken cancellationToken = default)
    {
        Result<PokemonDTO> result = await _mediator.Send(new AddPokemonCommand
        {
            OwnerId = request.OwnerId,
            PokemonId = request.PokemonId
        });
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.Contains("not found", StringComparison.OrdinalIgnoreCase))) return NotFound(result);
            return BadRequest(result);
        }
        return Created("Added Pokemon in Owner's Collection", result);
    }
}