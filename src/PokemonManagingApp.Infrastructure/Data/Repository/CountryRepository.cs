using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CountryRepository(ApplicationDBContext context, IMemoryCache memoryCache) : BaseRepository<Country>(context), ICountryRepository
{
  private readonly IMemoryCache _memoryCache = memoryCache;

  public async Task<IEnumerable<Country>> GetAllCountries(bool trackChanges)
  {
    string key = $"countries-all";
    return await _memoryCache.GetOrCreateAsync(key, async entry =>
    {
      entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
      IQueryable<Country> query = _dbSet.AsQueryable();
      query = trackChanges ? query : query.AsNoTracking();
      return await query
        .Include(c => c.Owners)
        .Where(c => c.Status)
        .ToListAsync();
    }) ?? [];
  }

  public async Task<Country?> GetCountryById(Guid id, bool trackChanges)
  {
    string key = $"countries-{id}";
    return await _memoryCache.GetOrCreateAsync(key, async entry =>
    {
      IQueryable<Country> query = _dbSet.AsQueryable();
      query = trackChanges ? query : query.AsNoTracking();
      return await query
        .Include(c => c.Owners)
        .Where(c => c.Status)
        .SingleOrDefaultAsync();
    });
  }
}