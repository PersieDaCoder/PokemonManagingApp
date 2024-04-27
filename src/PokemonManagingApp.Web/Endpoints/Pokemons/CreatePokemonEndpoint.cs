using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using PokemonManagingApp.UseCases.UseCase_Pokemons;
using PokemonManagingApp.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using PokemonManagingApp.Web.Helpers;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;
public record CreatePokemonRequest
{
    [FromBody] public required string Name { get; init; }
    [FromBody] public required DateTime BirthDate { get; init; }
    [FromBody] public required Guid CategoryId { get; init; }
    [FromBody] public required string Description { get; init; }
    [FromBody] public required string ImageUrl { get; init; }
    [FromBody] public required int Height { get; init; }
    [FromBody] public required int Weight { get; init; }
}
public class CreatePokemonEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreatePokemonRequest>.WithActionResult<Result<PokemonDTO>>
{
    private readonly IMediator _mediator = mediator;
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("/api/pokemons")]
    [SwaggerOperation(
        Summary = "Create a new Pokemon",
        Tags = ["Pokemons"]
    )]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public override async Task<ActionResult<Result<PokemonDTO>>> HandleAsync([FromBody] CreatePokemonRequest request, CancellationToken cancellationToken = default)
    {
        Result<PokemonDTO> result = await _mediator.Send(new CreatePokemonCommand
        {
            Name = request.Name,
            BirthDate = request.BirthDate,
            CategoryId = request.CategoryId,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Height = request.Height,
            Weight = request.Weight
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Created(result.SuccessMessage, result);
    }
}
