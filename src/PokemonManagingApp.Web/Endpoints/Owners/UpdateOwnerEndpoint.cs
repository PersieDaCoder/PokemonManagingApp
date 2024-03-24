using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.UpdateOwner;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record UpdateOwnerRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [FromBody] public string Name { get; set; } = null!;
    [Required(ErrorMessage = "GymId is required")]
    [FromBody] public Guid GymId { get; set; }
    [Required(ErrorMessage = "CountryId is required")]
    [FromBody] public required Guid CountryId { get; set; }
}
public class UpdateOwnerEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<UpdateOwnerRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPut]
    [Route("api/Owners/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [SwaggerOperation(
        Summary = "Update an owner",
        Description = "Update an owner",
        OperationId = "Owners.Update",
        Tags = ["Owners"]
    )]
    public override async Task<ActionResult> HandleAsync(UpdateOwnerRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new UpdateOwnerCommand
        {
            Id = request.Id,
            Name = request.Name,
            GymId = request.GymId,
            CountryId = request.CountryId
        });
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.Contains("not found"))) return NotFound(result);
            return BadRequest(result);
        }
        return NoContent();
    }
}