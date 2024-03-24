using Ardalis.Result;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

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
        return Result<PokemonDTO>.Success(pokemon.MapToDTO());
    }
}
