using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.CreateOwner;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record CreateOwnerRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string UserName { get; init; } = null!;
    public Guid GymId { get; init; }
    public Guid CountryId { get; init; }
}
public class CreateOwnerEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateOwnerRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("api/Owners")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [SwaggerOperation(
        Summary = "Create a new owner",
        Description = "Create a new owner",
        OperationId = "Owner.Create",
        Tags = ["Owners"]
    )]
    public override async Task<ActionResult> HandleAsync(CreateOwnerRequest request, CancellationToken cancellationToken = default)
    {
        Result<OwnerDTO> result = await _mediator.Send(new CreateOwnerCommand
        {
            CountryId = request.CountryId,
            UserName = request.UserName,
            Gmail = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            GymId = request.GymId
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Created("", result);
    }
}