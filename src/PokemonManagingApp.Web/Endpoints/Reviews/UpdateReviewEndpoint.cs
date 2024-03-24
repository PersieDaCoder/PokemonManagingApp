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
using PokemonManagingApp.UseCases.UseCase_Reviews.Commands.UpdateReview;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviews;

public record UpdateReviewRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; set; }
    [Required(ErrorMessage = "Text is required")]
    [FromBody] public string Text { get; set; } = null!;
    [Required(ErrorMessage = "Title is required")]
    [FromBody] public string Title { get; set; } = null!;
    [Required(ErrorMessage = "PokemonId is required")]
    [FromBody] public Guid PokemonId { get; set; }
}
public class UpdateReviewEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<UpdateReviewRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;
    [HttpPut]
    [Authorize]
    [Route("api/Reviews/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [SwaggerOperation(
        Summary = "Update a review",
        Tags = ["Reviews"]
    )]
    public override async Task<ActionResult> HandleAsync(UpdateReviewRequest request, CancellationToken cancellationToken = default)
    {
        Result result = await _mediator.Send(new UpdateReviewCommand
        {
            Id = request.Id,
            Text = request.Text,
            Title = request.Title,
            PokemonId = request.PokemonId
        });
        if (!result.IsSuccess)
        {
            if (result.Errors.Any(err => err.ToLower().Contains("not found"))) return NotFound(result);
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
}