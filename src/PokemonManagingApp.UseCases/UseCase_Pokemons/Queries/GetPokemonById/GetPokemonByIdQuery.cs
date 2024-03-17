using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons.Queries.GetPokemonById;

public class GetPokemonByIdQuery : IRequest<Result<PokemonDTO>>
{
  public Guid Id { get; init; }
}