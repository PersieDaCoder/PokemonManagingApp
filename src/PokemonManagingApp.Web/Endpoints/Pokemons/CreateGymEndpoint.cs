using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Gyms.Commands;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;

public record CreateGymRequest
{
    [Required(ErrorMessage = "Name is required")]
    [FromBody] public string Name { get; set; } = null!;
};
public class CreateGymEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateGymRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("api/Gyms")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [SwaggerOperation(
        Summary = "Create a new Gym",
        Tags = ["Gyms"]
    )]
    public override async Task<ActionResult> HandleAsync(CreateGymRequest request, CancellationToken cancellationToken = default)
    {
        Result<GymDTO> result = await _mediator.Send(new CreateGymCommand
        {
            Name = request.Name
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Created("Create Gym Successfully",result);
    }
}