using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.DisableCategory;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public record DisableCategoryRequest
{
    [FromRoute] public Guid Id { get; init; }
}
public class DisableCategoryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<DisableCategoryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("/api/categories/{Id}")]
    [SwaggerOperation(
        Summary = "Disable selected Category",
        Tags = ["Categories"]
    )]
    public override async Task<ActionResult> HandleAsync(DisableCategoryRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new DisableCategoryCommand
        {
            Id = request.Id
        });
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}