using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeCountry;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record ChangeCountryRequest
{
    [Required(ErrorMessage = "Country Id is required")]
    [FromRoute] public Guid CountryId { get; init; }
}
public class ChangeCountryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<ChangeCountryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

[HttpPut]
[Authorize]
[Route("api/owners/country/{CountryId:guid}")]
[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
[SwaggerOperation(
    Summary = "Change country of owner",
    Tags = ["Owners"]
)]
    public override async Task<ActionResult> HandleAsync(ChangeCountryRequest request, CancellationToken cancellationToken = default)
    {
        Guid currentOwnerId = Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid) ?? throw new ValidationException("Owner ID not found in token"));
        Result result = await _mediator.Send(new ChangeCountryCommand
        {
            OwnerId = currentOwnerId,
            CountryId = request.CountryId
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}