using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Reviews.Commands.DisableReview;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviews;

public record DisableReviewRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; set; }
}
public class DisableReviewEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<DisableReviewRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete]
    [Authorize]
    [Route("api/Reviews/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [SwaggerOperation(
          Summary = "Disable a review",
          Tags = ["Reviews"]
      )]
    public override async Task<ActionResult> HandleAsync(DisableReviewRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new DisableReviewCommand
        {
            Id = request.Id
        });
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.ToLower().Contains("not found"))) return NotFound(result);
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
}