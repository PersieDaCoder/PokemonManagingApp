using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.RemovePokemonOutOfCollection;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record RemovePokemonOutOfCollectionRequest
{
    [Required(ErrorMessage = "Pokemon Id is required")]
    [FromRoute] public Guid PokemonId { get; set; }
}
public class RemovePokemonOutOfCollectionEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<RemovePokemonOutOfCollectionRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete]
    [Authorize]
    [Route("api/owners/pokemons/{PokemonId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [SwaggerOperation(
          Summary = "Remove a Pokemon from the collection of an owner",
          Tags = ["Owners"]
      )]
    public override async Task<ActionResult> HandleAsync(RemovePokemonOutOfCollectionRequest request, CancellationToken cancellationToken = default)
    {
        Guid currentOwnerId = Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid) ??
            throw new ArgumentNullException("Owner Id is not found in the token claims"));
        Result result = await _mediator.Send(new RemovePokemonOutOfCollectionCommand
        {
            OwnerId = currentOwnerId,
            PokemonId = request.PokemonId
        });
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}