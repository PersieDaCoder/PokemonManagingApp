using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
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
        Tags = new[] { "Categories" }
    )]
    public override async Task<ActionResult> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken = default)
    {
        Result<CategoryDTO> result = await _mediator.Send(new UpdateCategoryCommand
        {
            Id = request.Id,
            Name = request.Name
        });
        return result.IsSuccess ? StatusCode(204,result) : BadRequest(result);
    }
}