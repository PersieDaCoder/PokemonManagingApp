using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.AddPokemonToCollection;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record AddPokemonToCollectionRequest
{
    [Required(ErrorMessage = "Pokemon Id is required")]
    [FromRoute] public Guid PokemonId { get; set; }
};
public class AddPokemonToCollectionEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<AddPokemonToCollectionRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize]
    [Route("api/owners/pokemons/{PokemonId:guid}")]
    [SwaggerOperation(
          Summary = "Add a Pokemon to the collection of an owner",
          Tags = ["Owners"]
      )]
    public override async Task<ActionResult> HandleAsync(AddPokemonToCollectionRequest request, CancellationToken cancellationToken = default)
    {
        Guid currentOwnerId = Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid) ?? throw new ArgumentNullException("User is not found"));
        Result<PokemonDTO> result = await _mediator.Send(new AddPokemonToCollectionCommand
        {
            OwnerId = currentOwnerId,
            PokemonId = request.PokemonId
        });
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Created("Added Pokemon in Owner's Collection", result);
    }
}