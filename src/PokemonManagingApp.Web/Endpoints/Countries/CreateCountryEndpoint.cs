
using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Countries.Commands.CreateCountry;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Countries;

public record CreateCountryRequest
{
    [Required(ErrorMessage = "Name is required")]
    [FromBody] public string Name { get; init; } = null!;
};
public class CreateCountryEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateCountryRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("api/Countries")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [SwaggerOperation(
        Summary = "Create a new country",
        Description = "Create a new country",
        OperationId = "Country.Create",
        Tags = ["Countries"]
    )]
    public override async Task<ActionResult> HandleAsync(CreateCountryRequest request, CancellationToken cancellationToken = default)
    {
        Result<CountryDTO> result = await _mediator.Send(new CreateCountryCommand
        {
            Name = request.Name,
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Created("",result);
    }
}