using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CategoryRepository(ApplicationDBContext context, IMemoryCache memoryCache) : BaseRepository<Category>(context), ICategoryRepository
{
  private readonly IMemoryCache _memoryCache = memoryCache;

  public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool checkTraces)
  {
    string key = "categories-all";
    return await _memoryCache.GetOrCreateAsync(key, async entry =>
    {
      entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
      IQueryable<Category> query = _dbSet.AsQueryable();
      query = checkTraces ? query : query.AsNoTracking();
      return await query
        .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
        .Where(c => c.Status)
        .ToListAsync();
    }) ?? [];
  }

  public async Task<Category?> GetCategoryByIdAsync(Guid id, bool checkTraces)
  {
    string key = $"categories-{id}";
    IQueryable<Category> query = _dbSet.AsQueryable();
    query = checkTraces ? query : query.AsNoTracking();
    return await query
      .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
      .Where(c => c.Status)
      .Where(c => c.Id.Equals(id))
      .SingleOrDefaultAsync();
  }
}