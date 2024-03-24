using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.AddPokemonToCategory;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public record AddPokemonToCategoryRequest
{
    [Required(ErrorMessage = "PokemonId is required")]
    [FromRoute] public Guid PokemonId { get; init; }
    [Required(ErrorMessage = "CategoryId is required")]
    [FromRoute] public Guid CategoryId { get; init; }
}
public class AddPokemonToCategoryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<AddPokemonToCategoryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Route("api/Categories/{CategoryId:guid}/Pokemons/{PokemonId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [SwaggerOperation(
        Summary = "Add a Pokemon to a Category",
        Tags = ["Categories"]
    )]
    public override async Task<ActionResult> HandleAsync(AddPokemonToCategoryRequest request, CancellationToken cancellationToken = default)
    {
        Result<PokemonDTO> result = await _mediator.Send(new AddPokemonToCategoryCommand
        {
            CategoryId = request.CategoryId,
            PokemonId = request.PokemonId
        }, cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.Contains("not found", StringComparison.OrdinalIgnoreCase)))
                return NotFound(result);
            return BadRequest(result);
        }
        return Created("", result);
    }
}