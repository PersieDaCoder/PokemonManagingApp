using Ardalis.Result;
using PokemonManagingApp.Core.DTOs.Pokemon;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases;
using MediatR;

namespace PokemonManagingApp.Pokemon_UseCase;

public class CreatePokemonHandler(IUnitOfWork context) : IRequestHandler<CreatePokemonCommand, Result<PokemonDTO>>
{
  private readonly IUnitOfWork _context = context;

  public async Task<Result<PokemonDTO>> Handle(CreatePokemonCommand request, CancellationToken cancellationToken)
    {
        Pokemon pokemon = new()
        {
            Name = request.Name,
            BirthDate = request.BirthDate
        };
        _context.PokemonRepository.Add(pokemon);
        await _context.SaveChangesAsync();
        return Result<PokemonDTO>.Success(new PokemonDTO
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            BirthDate = pokemon.BirthDate
        });
    }
}
