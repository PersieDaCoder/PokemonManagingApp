using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
    var expireTime = TimeSpan.FromMinutes(2);
    _cacheService.SetData(key, pokemons, expireTime);
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
    var expireTime = TimeSpan.FromMinutes(2);
    _cacheService.SetData(key, pokemon, expireTime);
    return pokemon;
  }
}