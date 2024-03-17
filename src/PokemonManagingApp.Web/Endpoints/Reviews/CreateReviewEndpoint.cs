using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.UseCase_Reviews.Commands.CreateReview;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviews;

public record CreateReviewRequest
{
    [Required(ErrorMessage = "Text is required")]
    public string Text { get; set; } = null!;
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "ReviewerId is required")]
    public Guid ReviewerId { get; set; }
    [Required(ErrorMessage = "PokemonId is required")]
    public Guid PokemonId { get; set; }
}

public class CreateReviewEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateReviewRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Route("api/Reviews")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [SwaggerOperation(
        Summary = "Create a review",
        Tags = ["Reviews"]
    )]
    public override async Task<ActionResult> HandleAsync(CreateReviewRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new CreateReviewCommand
        {
            Text = request.Text,
            Title = request.Title,
            ReviewerId = request.ReviewerId,
            PokemonId = request.PokemonId
        });
        if (!result.IsSuccess) return BadRequest(result.Errors);
        return Created("", result);
    }
}