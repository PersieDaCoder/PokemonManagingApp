using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class PokemonRepository(ApplicationDBContext context, IMemoryCache memoryCache) : BaseRepository<Pokemon>(context), IPokemonRepository
{
  private readonly IMemoryCache _memoryCache = memoryCache;

  public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(bool checkTraces)
  {
    string key = "pokemons-all";
    return await _memoryCache.GetOrCreateAsync(key, async entry =>
    {
      entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
      IQueryable<Pokemon> query = _dbSet.AsQueryable();
      query = checkTraces ? query : query.AsNoTracking();
      return await query
        .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
        .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
        .Include(p => p.Reviews).ThenInclude(r => r.Reviewer)
        .ToListAsync();
    }) ?? [];
  }

  public async Task<Pokemon?> GetPokemonByIdAsync(Guid id, bool checkTraces)
  {
    string key = $"pokemons-{id}";
    return await _memoryCache.GetOrCreateAsync(key, async entry =>
    {
      entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
      IQueryable<Pokemon> query = _dbSet.AsQueryable();
      query = checkTraces ? query : query.AsNoTracking();
      return await query
        .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
        .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
        .Include(p => p.Reviews).ThenInclude(r => r.Reviewer)
        .FirstOrDefaultAsync(p => p.Id.Equals(id));
    });
  }
}