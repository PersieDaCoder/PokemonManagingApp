using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories.Queries.GetPokemonsByCategoryId;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;

public record GetPokemonByCategoryIdRequest
{
    [Required(ErrorMessage = "CategoryId is required")]
    [FromRoute] public Guid CategoryId { get; init; }
}
public class GetPokemonByCategoryIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<GetPokemonByCategoryIdRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Route("api/Categories/{CategoryId}/Pokemons")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get pokemons by category id",
        Tags = ["Pokemons"]
    )]
    public override async Task<ActionResult> HandleAsync(GetPokemonByCategoryIdRequest request, CancellationToken cancellationToken = default)
    {
        Result<IEnumerable<PokemonDTO>> result = await _mediator.Send(new GetPokemonByCategoryIdQuery { CategoryId = request.CategoryId }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Ok(result);
    }
}