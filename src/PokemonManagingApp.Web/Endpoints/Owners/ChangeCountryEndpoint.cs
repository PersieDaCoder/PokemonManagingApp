using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.ChangeCountry;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record ChangeCountryRequest
{
    [Required(ErrorMessage = "Owner Id is required")]
    [FromRoute] public Guid OwnerId { get; init; }
    [Required(ErrorMessage = "Country Id is required")]
    [FromRoute] public Guid CountryId { get; init; }
}
public class ChangeCountryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<ChangeCountryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

[HttpPut]
[Route("owners/{OwnerId:guid}/country/{CountryId:guid}")]
[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
[SwaggerOperation(
    Summary = "Change country of owner",
    Tags = ["Owners"]
)]
    public override async Task<ActionResult> HandleAsync(ChangeCountryRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new ChangeCountryCommand
        {
            OwnerId = request.OwnerId,
            CountryId = request.CountryId
        }, cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(error => error.Contains("not found", StringComparison.OrdinalIgnoreCase)))
                return NotFound(result);
            return BadRequest(result);
        }
        return NoContent();
    }
}