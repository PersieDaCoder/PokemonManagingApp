using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class PokemonRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Pokemon>(context, cacheService), IPokemonRepository
{
  public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken cancellationToken = default)
  {
    string key = "pokemons-all";
    // get pokemons from cache
    IEnumerable<Pokemon>? cachedPokemons = _cacheService.GetData<IEnumerable<Pokemon>>(key);
    if (cachedPokemons is not null) return cachedPokemons;
    // get pokemons from database
    IEnumerable<Pokemon> pokemons = await _context
      .Pokemons
      .AsNoTracking()
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
      .Include(p => p.Reviews)
      .ToListAsync(cancellationToken);
    // set pokemons to cache
    _cacheService.SetData(key, pokemons);
    return pokemons;
  }

  public async Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    // get pokemon from cache
    string key = $"pokemon-{id}";
    Pokemon? cachedPokemon = _cacheService.GetData<Pokemon>(key);
    if (cachedPokemon is not null) return cachedPokemon;
    // get pokemon from database
    Pokemon? pokemon = await _context
      .Pokemons
      .AsNoTracking()
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
      .Include(p => p.Reviews)
      .FirstOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
    if (pokemon is null) return null!;
    // set pokemon to cache
    _cacheService.SetData(key, pokemon);
    return pokemon;
  }

  public async Task<IEnumerable<Pokemon>> GetPokemonsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
  {
    string key = $"pokemons-category-{categoryId}";
    // get pokemons from cache
    IEnumerable<Pokemon>? cachedPokemons = _cacheService.GetData<IEnumerable<Pokemon>>(key);
    if (cachedPokemons is not null) return cachedPokemons;
    // get pokemons from database
    IEnumerable<Pokemon> pokemons = await _context
      .Pokemons
      .AsNoTracking()
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(owner => owner == null ? null! : owner.Gym)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(owner => owner == null ? null! : owner.Country)
      .Include(p => p.Reviews)
      .Where(p => p.PokemonCategories.Any(pc => pc.CategoryId.Equals(categoryId) && !pc.IsDeleted))
      .Where(p => !p.IsDeleted)

      .ToListAsync(cancellationToken);
    // set pokemons to cache
    _cacheService.SetData(key, pokemons);
    return pokemons;
  }

  public async Task<IEnumerable<Pokemon>> GetPokemonsByOwnerIdAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    string key = $"pokemons-collection-{userId}";
    // get pokemons from cache
    IEnumerable<Pokemon>? cachedPokemons = _cacheService.GetData<IEnumerable<Pokemon>>(key);
    if (cachedPokemons is not null) return cachedPokemons;
    // get pokemons from database
    IEnumerable<Pokemon> pokemons = await _context
      .Pokemons
      .AsNoTracking()
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(owner => owner == null ? null! : owner.Gym)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner).ThenInclude(owner => owner == null ? null! : owner.Country)
      .Include(p => p.Reviews)
      .Where(p => p.PokemonOwners.Any(po => po.OwnerId.Equals(userId) && !po.IsDeleted))
      .Where(p => !p.IsDeleted)
      .ToListAsync(cancellationToken);
    // set pokemons to cache
    _cacheService.SetData(key, pokemons);
    return pokemons;
  }
}