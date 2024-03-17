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
    public string Name { get; set; } = null!;
    public string Gym { get; set; } = null!;
    public Guid CountryId { get; set; }
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
            Name = request.Name,
            CountryId = request.CountryId,
            Gym = request.Gym,
        }, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result);
        return Created("", result);
    }
}