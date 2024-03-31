using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class PokemonRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Pokemon>(context, cacheService), IPokemonRepository
{
  public async Task<IEnumerable<PokemonDTO>> GetAllPokemonsAsync(CancellationToken cancellationToken = default)
  {
    string key = "pokemons-all";
    // get pokemons from cache
    IEnumerable<PokemonDTO>? cachedPokemons = _cacheService.GetData<IEnumerable<PokemonDTO>>(key);
    if (cachedPokemons is not null) return cachedPokemons;
    // get pokemons from database
    IEnumerable<PokemonDTO> pokemons = await _context
      .Pokemons
      .AsNoTracking()
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
      .Include(p => p.Reviews)
      .Select(pokemon => pokemon.MapToDTO())
      .ToListAsync(cancellationToken);
    // set pokemons to cache
    _cacheService.SetData(key, pokemons);
    return pokemons;
  }

  public async Task<PokemonDTO> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    // get pokemon from cache
    string key = $"pokemon-{id}";
    PokemonDTO? cachedPokemon = _cacheService.GetData<PokemonDTO>(key);
    if (cachedPokemon is not null) return cachedPokemon;
    // get pokemon from database
    PokemonDTO? pokemon = await _context
      .Pokemons
      .AsNoTracking()
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
      .Include(p => p.Reviews)
      .Select(pokemon => pokemon.MapToDTO())
      .FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
    if (pokemon is null) return null!;
    // set pokemon to cache
    _cacheService.SetData(key, pokemon);
    return pokemon;
  }

  public async Task<IEnumerable<PokemonDTO>> GetPokemonsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
  {
    string key = $"pokemons-category-{categoryId}";
    // get pokemons from cache
    IEnumerable<PokemonDTO>? cachedPokemons = _cacheService.GetData<IEnumerable<PokemonDTO>>(key);
    if (cachedPokemons is not null) return cachedPokemons;
    // get pokemons from database
    IEnumerable<PokemonDTO> pokemons = await _context
      .Pokemons
      .AsNoTracking()
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(owner => owner == null ? null! : owner.Gym)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(owner => owner == null ? null! : owner.Country)
      .Include(p => p.Reviews)
      .Where(p => p.PokemonCategories.Any(pc => pc.CategoryId.Equals(categoryId) && !pc.IsDeleted))
      .Where(p => !p.IsDeleted)
      .Select(pokemon => pokemon.MapToDTO())
      .ToListAsync(cancellationToken);
    // set pokemons to cache
    _cacheService.SetData(key, pokemons);
    return pokemons;
  }

  public async Task<IEnumerable<PokemonDTO>> GetPokemonsByOwnerIdAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    string key = $"pokemons-collection-{userId}";
    // get pokemons from cache
    IEnumerable<PokemonDTO>? cachedPokemons = _cacheService.GetData<IEnumerable<PokemonDTO>>(key);
    if (cachedPokemons is not null) return cachedPokemons;
    // get pokemons from database
    IEnumerable<PokemonDTO> pokemons = await _context
      .Pokemons
      .AsNoTracking()
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(owner => owner == null ? null! : owner.Gym)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(owner => owner == null ? null! : owner.Country)
      .Include(p => p.Reviews)
      .Where(p => p.PokemonOwners.Any(po => po.OwnerId.Equals(userId) && !po.IsDeleted))
      .Where(p => !p.IsDeleted)
      .Select(pokemon => pokemon.MapToDTO())
      .ToListAsync(cancellationToken);
    // set pokemons to cache
    _cacheService.SetData(key, pokemons);
    return pokemons;
  }
}