using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
  public CategoryRepository(ApplicationDBContext context) : base(context)
  {
  }


  public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool checkTraces)
  {
    IQueryable<Category> query = _dbSet.AsQueryable();
    query = checkTraces ? query : query.AsNoTracking();
    return await query
      .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
      .Where(c => c.Status)
      .ToListAsync();
  }

  public async Task<Category?> GetCategoryByIdAsync(Guid id, bool checkTraces)
  {
    IQueryable<Category> query = _dbSet.AsQueryable();
    query = checkTraces ? query : query.AsNoTracking();
    return await query
      .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
      .Where(c => c.Status)
      .Where(c => c.Id.Equals(id))
      .SingleOrDefaultAsync();
  }
}