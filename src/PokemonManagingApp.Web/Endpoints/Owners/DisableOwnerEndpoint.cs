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
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.DisableOwner;
using PokemonManagingApp.Web.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;
public record DisableOwnerRequest
{
  [Required(ErrorMessage = "Id is required")]
  [FromRoute] public Guid Id { get; init; }
}
public class DisableOwnerEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<DisableOwnerRequest>.WithActionResult
{
  private readonly IMediator _mediator = mediator;


  [HttpDelete]
  [Authorize(Roles = "Admin")]
  [Route("api/Owners/{Id}")]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
  [SwaggerOperation(
    Summary = "Disable an Owner",
    Tags = ["Owners"]
  )]
  public override async Task<ActionResult> HandleAsync(DisableOwnerRequest request, CancellationToken cancellationToken = default)
  {
    Result result = await _mediator.Send(new DisableOwnerCommand
    {
      Id = request.Id
    }, cancellationToken);
    if (!result.IsSuccess) return result.IsNotFound() ? NotFound(result) : BadRequest(result);
    return NoContent();
  }
}