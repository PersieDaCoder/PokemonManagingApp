using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeGym;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record ChangeGymRequest
{
    [Required(ErrorMessage = "Gym Id is required")]
    [FromBody] public Guid GymId { get; set; }
}
public class ChangeGymEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<ChangeGymRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPut]
    [Authorize]
    [Route("api/Owners/change-gym")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [SwaggerOperation(
        Summary = "Change Owner's Gym",
        Tags = ["Owners"]
    )]
    public override async Task<ActionResult> HandleAsync(ChangeGymRequest request, CancellationToken cancellationToken = default)
    {
        Guid currentOwnerId = Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid) ?? throw new ValidationException("Token is not valid"));
        Result result = await _mediator.Send(new ChangeGymCommand
        {
            OwnerId = currentOwnerId,
            GymId = request.GymId
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}