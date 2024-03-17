using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Reviewers.Commands.CreateReviewer;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviewers;

public record CreateReviewerRequest
{
    [Required(ErrorMessage = "First name is required")]
    [FromBody] public string FirstName { get; init; } = null!;
    [Required(ErrorMessage = "Last name is required")]
    [FromBody] public string LastName { get; init; } = null!;
}
public class CreateReviewerEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateReviewerRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Route("api/Reviewers")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [SwaggerOperation(
        Summary = "Create a new reviewer",
        Tags = ["Reviewers"]
    )]
    public override async Task<ActionResult> HandleAsync(CreateReviewerRequest request, CancellationToken cancellationToken = default)
    {
        Result<ReviewerDTO> result = await _mediator.Send(new CreateReviewerCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        }, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result);
        return Created("Created Reviewer Successfully", result);
    }
}