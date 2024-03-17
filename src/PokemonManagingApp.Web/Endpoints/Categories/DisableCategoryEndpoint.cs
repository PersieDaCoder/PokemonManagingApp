using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.DisableCategory;
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
    [Route("/api/Categories/{Id}")]
    [SwaggerOperation(
        Summary = "Disable selected Category",
        Tags = new[] { "Categories" }
    )]
    public override async Task<ActionResult> HandleAsync(DisableCategoryRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new DisableCategoryCommand
        {
            Id = request.Id
        });
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.ToLower().Contains("not found"))) return NotFound(result);
            return BadRequest(result);
        }
        return NoContent();
    }
}