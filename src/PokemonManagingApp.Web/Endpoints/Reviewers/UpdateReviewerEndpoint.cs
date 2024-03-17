using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.UpdateReviewer;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviewers;

public record UpdateReviewerRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; init; }
    [Required(ErrorMessage = "First name is required")]
    [FromBody] public string FirstName { get; init; } = null!;
    [Required(ErrorMessage = "Last name is required")]
    [FromBody] public string LastName { get; init; } = null!;
}
public class UpdateReviewerEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<UpdateReviewerRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;
    
    [HttpPut]
    [Route("api/Reviewers/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [SwaggerOperation(
        Summary = "Update a reviewer",
        Tags = ["Reviewers"]
    )]
    public override async Task<ActionResult> HandleAsync(UpdateReviewerRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new UpdateReviewerCommand
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName
        }, cancellationToken);
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.Contains("not found", StringComparison.OrdinalIgnoreCase))) return NotFound(result);
            return BadRequest(result);
        }
        return NoContent();
    }
}