using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.CreateCategory;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public record CreateCategoryRequest
{
  [Required(ErrorMessage = "Category Name is required")]
  [FromBody] public string Name { get; init; } = null!;
}
public class CreateCategoryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateCategoryRequest>.WithActionResult
{
  private readonly IMediator _mediator = mediator;

  [HttpPost]
  [Authorize(Roles = "Admin")]
  [Route("/api/Categories")]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
  [SwaggerOperation(
        Summary = "Create a new Category",
        Tags = ["Categories"]
    )]
  public override async Task<ActionResult> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default)
  {
    Result<CategoryDTO> result = await _mediator.Send(new CreateCategoryCommand
    {
      Name = request.Name
    });
    if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
    return Created();
  }
}