﻿using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonManagingApp.UseCase.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using PokemonManagingApp.UseCases.UseCase_Pokemons;

namespace PokemonManagingApp.Web.Endpoints.Pokemons;
public record CreatePokemonRequest
{
    [FromBody] public required string Name { get; init; }
    [FromBody] public required DateTime BirthDate { get; init; }
}
public class CreatePokemonEndpoint(IMediator mediator) : EndpointBaseAsync.WithRequest<CreatePokemonRequest>.WithActionResult<Result<PokemonDTO>>
{
    private readonly IMediator _mediator = mediator;
    [HttpPost]
    [Route("/api/Pokemons")]
    [SwaggerOperation(
        Summary = "Create a new Pokemon",
        Tags = ["Pokemons"]
    )]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public override async Task<ActionResult<Result<PokemonDTO>>> HandleAsync([FromBody]CreatePokemonRequest request, CancellationToken cancellationToken = default)
    {
        Result<PokemonDTO> result = await _mediator.Send(new CreatePokemonCommand
        {
            Name = request.Name,
            BirthDate = request.BirthDate
        }, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
