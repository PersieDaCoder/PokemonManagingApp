using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Reviews;

public class GetReviewsByOwnerIdEndpoint : EndpointBaseAsync.WithoutRequest.WithActionResult
{
    [HttpGet]
    [Authorize]
    [Route("api/Reviews/GetReviewsByOwnerId")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [SwaggerOperation(
        Summary = "Get reviews by owner id",
        Tags = ["Reviews"]
    )]
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}