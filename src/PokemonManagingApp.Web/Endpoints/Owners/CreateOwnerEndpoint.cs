using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.CreateOwner;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record CreateOwnerRequest
{
    public string Email {get;init;} = null!;
    public string Password { get; init; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string UserName { get; init; } = null!;
    public string Gym { get; init; } = null!;
    public Guid CountryId { get; init; }
}
public class CreateOwnerEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateOwnerRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
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
            Gym = request.Gym
        }, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result);
        return Created("", result);
    }
}