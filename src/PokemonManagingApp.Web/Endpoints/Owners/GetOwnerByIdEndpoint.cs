using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetOwnerById;
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
        if (!result.IsSuccess) return NotFound(result);
        return Ok(result);
    }
}