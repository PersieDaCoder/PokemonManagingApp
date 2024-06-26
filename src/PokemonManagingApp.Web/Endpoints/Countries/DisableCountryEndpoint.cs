using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Countries.Commands.DisableCountry;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Countries;

public record DisableCountryRequest
{
    [Required(ErrorMessage = "Country Id is required")]
    [FromRoute] public Guid Id { get; init; }
}
public class DisableCountryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<DisableCountryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("api/countries/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [SwaggerOperation(
        Summary = "Disable a country",
        Tags = ["Countries"]
    )]
    public override async Task<ActionResult> HandleAsync(DisableCountryRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new DisableCountryCommand
        {
            Id = request.Id
        });
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}