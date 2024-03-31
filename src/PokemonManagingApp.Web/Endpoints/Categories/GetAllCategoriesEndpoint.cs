using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public class GetAllCategoriesEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult<Result>
{
  private readonly IMediator _mediator = mediator;

  [HttpGet]
  [Authorize]
  [Route("api/Categories")]
  [SwaggerOperation(
    Summary = "Get All Categories",
    Tags = new[] { "Categories" }
  )]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
  public override async Task<ActionResult<Result>> HandleAsync(CancellationToken cancellationToken = default)
  {
    Result<IEnumerable<CategoryDTO>> result =
        await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken);
    if(!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
    return Ok(result);
  }
}
