using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Pokemons;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;
public record DisablePokemonRequest
{
    [FromRoute] public Guid Id { get; set; }
};
public class DisablePokemonEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<DisablePokemonRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("api/pokemons/{Id:Guid}/disable")]
    [SwaggerOperation(
        Summary = "Disable selected Pokemon",
        Tags = ["Pokemons"]
    )]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public override async Task<ActionResult> HandleAsync(DisablePokemonRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await Task.Run(() => _mediator.Send(new DisablePokemonCommand
        {
            Id = request.Id
        }, cancellationToken).Result);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}
