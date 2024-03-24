using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetOwnerById;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record GetOwnerByIdRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; set; }
};
public class GetOwnerByIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<GetOwnerByIdRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;
    [HttpGet]
    [Route("/api/Owners/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
    Summary = "Get Owner by Id",
    Tags = ["Owners"])]
    public override async Task<ActionResult> HandleAsync(GetOwnerByIdRequest request, CancellationToken cancellationToken = default)
    {
        Result<OwnerDTO> result = await _mediator.Send(new GetOwnerByIdQuery
        {
            Id = request.Id
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Ok(result);
    }
}