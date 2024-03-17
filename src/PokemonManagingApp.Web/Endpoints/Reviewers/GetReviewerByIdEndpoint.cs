using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.UseCase_Reviewers.Queries.GetReviewerById;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviewers;

public record GetReviewerByIdRequest
{
    [Required(ErrorMessage = "Id is required")]
    [FromRoute] public Guid Id { get; init; }
}
public class GetReviewerByIdEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<GetReviewerByIdRequest>.WithActionResult
{
    private readonly IMediator _mediator = mediator;
    [HttpGet]
    [Route("api/Reviewers/{Id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
            Summary = "Get all reviewers",
            Tags = ["Reviewers"]
        )]
    public override async Task<ActionResult> HandleAsync(GetReviewerByIdRequest request, CancellationToken cancellationToken = default)
    {
        Result<ReviewerDTO> result = await _mediator.Send(new GetReviewerByIdQuery { Id = request.Id }, cancellationToken);
        if (!result.IsSuccess) return NotFound(result);
        return Ok(result);
    }
}