using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Categories.Queries.GetCategoryById;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Categories;

public record GetCategoryByIdRequest
{
    [FromRoute] public Guid Id { get; set; }
}
public class GetCategoryByIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<GetCategoryByIdRequest>.WithActionResult<Result<CategoryDTO>>
{
    private readonly IMediator _mediator = mediator;
    [HttpGet]
    [Route("api/Categories/{Id}")]
    [SwaggerOperation(
        Summary = "Get Category By Its Id",
        Tags = new[] {"Categories"}
    )]
    public override async Task<ActionResult<Result<CategoryDTO>>> HandleAsync(GetCategoryByIdRequest request, CancellationToken cancellationToken = default)
    {
        Result<CategoryDTO> result = await _mediator.Send(new GetCategoryByIdQuery
        {
            Id = request.Id,
        });
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }
}