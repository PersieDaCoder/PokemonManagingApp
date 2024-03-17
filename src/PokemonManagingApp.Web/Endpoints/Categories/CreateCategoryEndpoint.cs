using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories.Commands.CreateCategory;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public record CreateCategoryRequest
{
  [Required(ErrorMessage = "Category Name is required")]
  [FromRoute] public string Name { get; init; } = null!;
}
public class CreateCategoryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateCategoryRequest>.WithActionResult
{
  private readonly IMediator _mediator = mediator;

  [HttpPost]
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
    if (!result.IsSuccess)
    {
      if (result.Errors.Any(err => err.ToLower().Contains("not found"))) return NotFound(result);
      return BadRequest(result);
    }
    return Created();
  }
}