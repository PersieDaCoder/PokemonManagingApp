using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Countries.Commands.UpdateCountry;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Countries;

public record UpdateCountryRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [FromBody] public string Name { get; init; } = null!;
}
public class UpdateCountryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<UpdateCountryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("/api/Countries/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [SwaggerOperation(
          Summary = "Update Country",
          Tags = ["Countries"]
      )]
    public override async Task<ActionResult> HandleAsync(UpdateCountryRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new UpdateCountryCommand
        {
            Id = request.Id,
            Name = request.Name
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return NoContent();
    }
}