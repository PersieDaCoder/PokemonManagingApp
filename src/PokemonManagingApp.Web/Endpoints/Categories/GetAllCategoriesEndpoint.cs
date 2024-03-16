using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public class GetAllCategoriesEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult<Result>
{
  private readonly IMediator _mediator = mediator;

  [HttpGet]
  [Route("api/Categories")]
  [SwaggerOperation(
    Summary = "Get All Categories",
    Tags = new[] { "Categories" }
  )]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
  public override async Task<ActionResult<Result>> HandleAsync(CancellationToken cancellationToken = default)
  {
    Result<IEnumerable<CategoryDTO>> getAllCategoriesResult =
        await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
    return getAllCategoriesResult.IsSuccess ? Ok(getAllCategoriesResult) : NotFound(getAllCategoriesResult);
  }
}
