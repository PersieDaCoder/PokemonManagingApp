using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.UpdateCategory;
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
    [Route("/api/Categories/{Id}")]
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
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.ToLower().Contains("not found"))) return NotFound(result);
            return BadRequest(result);
        }
        return NoContent();
    }
}