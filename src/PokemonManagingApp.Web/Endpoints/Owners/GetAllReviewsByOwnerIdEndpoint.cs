using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Queries.GetAllReviewsByOwnerId;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public class GetAllReviewsByOwnerIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithoutRequest.WithActionResult
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Authorize]
    [Route("api/owners/reviews")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get all reviews by owner id",
        Tags = ["Owners"]
    )]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        Guid currentUserId = Guid.Parse(HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sid) ?? throw new ValidationException("JWT is not found"));
        Result<IEnumerable<ReviewDTO>> result = await _mediator.Send(new GetAllReviewByOwnerIdQuery
        {
            OwnerId = currentUserId
        }, cancellationToken);
        if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
        return Ok(result.Value);
    }
}