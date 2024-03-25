using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.UseCase_Reviews.Commands.CreateReview;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviews;

public record CreateReviewRequest
{
    [Required(ErrorMessage = "Text is required")]
    public string Text { get; set; } = null!;
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "PokemonId is required")]
    public Guid PokemonId { get; set; }
}

public class CreateReviewEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreateReviewRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize]
    [Route("api/Reviews")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    [SwaggerOperation(
        Summary = "Create a review",
        Tags = ["Reviews"]
    )]
    public override async Task<ActionResult> HandleAsync(CreateReviewRequest request, CancellationToken cancellationToken = default)
    {
        Guid currentOwnerId = Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid) ?? throw new ValidationException("Token is not valid"));
        Result<ReviewDTO> result = await _mediator.Send(new CreateReviewCommand
        {
            Text = request.Text,
            Title = request.Title,
            PokemonId = request.PokemonId,
        });
        if (!result.IsSuccess) return BadRequest(result.Errors);
        return Created("", result);
    }
}