using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetGymById;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Gyms;

public record GetGymByIdRequest
{
    [Required(ErrorMessage = "Gym Id is required")]
    [FromRoute]public Guid Id { get; set; }
}
public class GetGymByIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<GetGymByIdRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Authorize]
    [Route("api/Gyms/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
            Summary = "Get Gym by Id",
            Tags = ["Gyms"]
        )]
    public override async Task<ActionResult> HandleAsync(GetGymByIdRequest request, CancellationToken cancellationToken = default)
    {
        Result<GymDTO> result = await _mediator.Send(new GetGymByIdQuery
        {
            Id = request.Id
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Ok(result);
    }
}