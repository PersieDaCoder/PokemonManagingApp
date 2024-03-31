using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.AddPokemonToCategory;
using PokemonManagingApp.Web.Helpers;
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
    [Authorize(Roles = "Admin")]
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
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Created("", result);
    }
}