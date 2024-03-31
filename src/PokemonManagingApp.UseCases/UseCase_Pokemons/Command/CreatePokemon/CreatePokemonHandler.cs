using Ardalis.Result;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Pokemons;

public class CreatePokemonHandler(IUnitOfWork context) : IRequestHandler<CreatePokemonCommand, Result<PokemonDTO>>
{
  private readonly IUnitOfWork _context = context;

  public async Task<Result<PokemonDTO>> Handle(CreatePokemonCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _context.CategoryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.CategoryId), false);
        if (category is null) return Result.NotFound("Category is not found");
        Guid pokemonId = Guid.NewGuid();
        Pokemon pokemon = new()
        {
            Id = pokemonId,
            Name = request.Name,
            BirthDate = request.BirthDate,
            PokemonCategories = [new PokemonCategory{
                CategoryId = request.CategoryId,
                PokemonId = pokemonId,
            }]
        };
        _context.PokemonRepository.Add(pokemon);
        await _context.SaveChangesAsync();
        return Result.Success(pokemon.MapToDTO());
    }
}
