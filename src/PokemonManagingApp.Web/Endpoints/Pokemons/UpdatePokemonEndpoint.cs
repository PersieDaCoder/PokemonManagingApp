using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Pokemon_UseCase;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;

public record UpdatePokemonRequest
{
    [FromRoute] public required Guid Id { get; init; }
    [FromBody] public required string Name { get; init; }
    [FromBody] public required DateTime BirthDate { get; init; }
}
public class UpdatePokemonEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<UpdatePokemonRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update selected Pokemon",
        Tags = ["Pokemons"]
    )]
    [Route("/api/Pokemons/{Id:guid}")]
    public override async Task<ActionResult> HandleAsync(UpdatePokemonRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new UpdatePokemonCommand
        {
            Id = request.Id,
            Name = request.Name,
            BirthDate = request.BirthDate
        });
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}