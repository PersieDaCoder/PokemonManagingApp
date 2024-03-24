using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.RemovePokemonOutOfCategory;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public record RemovePokemonOutOfCategoryRequest
{
    [Required(ErrorMessage = "CategoryId is required")]
    [FromRoute] public Guid CategoryId { get; init; }
    [Required(ErrorMessage = "PokemonId is required")]
    [FromRoute] public Guid PokemonId { get; init; }
}
public class RemovePokemonOutOfCategoryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<RemovePokemonOutOfCategoryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete]
    [Authorize]
    [Route("api/Categories/{CategoryId:guid}/Pokemons/{PokemonId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [SwaggerOperation(
        Summary = "Remove a Pokemon from a Category",
        Tags = new[] { "Categories" }
    )]
    public override async Task<ActionResult> HandleAsync(RemovePokemonOutOfCategoryRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new RemovePokemonOutOfCategoryCommand
        {
            CategoryId = request.CategoryId,
            PokemonId = request.PokemonId
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}