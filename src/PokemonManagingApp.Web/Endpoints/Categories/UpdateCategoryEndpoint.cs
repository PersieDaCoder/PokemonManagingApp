using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.UpdateCategory;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public record UpdateCategoryRequest
{
    [FromRoute] public Guid Id { get; init; }
    [FromBody] public string Name { get; init; } = null!;
}
public class UpdateCategoryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<UpdateCategoryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPut]
    [Route("/api/categories/{Id}")]
    [Authorize(Roles = "Admin")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [SwaggerOperation(
        Summary = "Update selected Category",
        Tags = ["Categories"]
    )]
    public override async Task<ActionResult> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        Result<CategoryDTO> result = await _mediator.Send(new UpdateCategoryCommand
        {
            Id = request.Id,
            Name = request.Name
        });
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}