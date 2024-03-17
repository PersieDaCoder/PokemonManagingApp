using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.DisableReviewer;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviewers;

public record DisableReviewerRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; init; }
}
public class DisableReviewerEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<DisableReviewerRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;
[HttpDelete]
[Route("api/Reviewers/{Id}")]
[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
[SwaggerOperation(
    Summary = "Disable a reviewer",
    Tags = ["Reviewers"]
)]
    public override async Task<ActionResult> HandleAsync(DisableReviewerRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new DisableReviewerCommand
        {
            Id = request.Id
        }, cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.Contains("not found", StringComparison.OrdinalIgnoreCase))) return NotFound(result);
            return BadRequest(result);
        }
        return NoContent();
    }
}
