using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CategoryRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Category>(context, cacheService), ICategoryRepository
{
  public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
  {
    //get categories from cache
    string key = "categories-all";
    IEnumerable<Category>? cacheCategories = _cacheService.GetData<IEnumerable<Category>>(key);
    if (cacheCategories is not null) return cacheCategories;
    // get categories from db
    IEnumerable<Category> categories = await _context
        .Categories.AsNoTracking()
        .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
        .Where(c => !c.IsDeleted)
        .ToListAsync(cancellationToken);
    // set categories to cache
    _cacheService.SetData(key, categories);
    return categories;
  }

  public async Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    //get category from cache
    string key = $"category-{id}";
    Category? cacheCategory = _cacheService.GetData<Category>(key);
    if (cacheCategory is not null) return cacheCategory;
    // get category from db
    Category? category = await _context
      .Categories.AsNoTracking()
      .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
       .Where(c => !c.IsDeleted)
      .Where(c => c.Id.Equals(id))
      .SingleOrDefaultAsync(cancellationToken);
    if (category is null) return null!;
    // set category to cache
    _cacheService.SetData(key, category);
    return category;
  }
}