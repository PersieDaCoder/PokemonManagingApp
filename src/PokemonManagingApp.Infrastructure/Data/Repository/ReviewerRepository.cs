using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class ReviewerRepository(ApplicationDBContext context, IMemoryCache memoryCache) : BaseRepository<Reviewer>(context), IReviewerRepository
{
  private readonly IMemoryCache _memoryCache = memoryCache;

  public async Task<IEnumerable<Reviewer>> GetAllReviewersAsync(bool checkTraces)
  {
    string key = "reviewers-all";
    return await _memoryCache.GetOrCreateAsync(key, async entry =>
    {
      entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
      IQueryable<Reviewer> query = _dbSet.AsQueryable();
      query = checkTraces ? query : query.AsNoTracking();
      return await query
        .Include(r => r.Reviews).ThenInclude(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
        .Include(r => r.Reviews).ThenInclude(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonOwners).ThenInclude(pc => pc.Owner)
        .Where(r => r.Status)
        .ToListAsync();
    }) ?? [];
  }

  public async Task<Reviewer?> GetReviewerByIdAsync(Guid id, bool checkTraces)
  {
    string key = $"reviewers-{id}";
    return await _memoryCache.GetOrCreateAsync(key, async entry =>
    {
      entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
      IQueryable<Reviewer> query = _dbSet.AsQueryable();
      query = checkTraces ? query : query.AsNoTracking();
      return await query
        .Include(r => r.Reviews).ThenInclude(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
        .Include(r => r.Reviews).ThenInclude(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonOwners).ThenInclude(pc => pc.Owner)
        .Where(r => r.Id.Equals(id))
        .Where(r => r.Status)
        .SingleOrDefaultAsync();
    });
  }
}