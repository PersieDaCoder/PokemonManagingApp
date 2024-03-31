using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CategoryRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Category>(context, cacheService), ICategoryRepository
{
  public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
  {
    //get categories from cache
    string key = "categories-all";
    IEnumerable<CategoryDTO>? cacheCategories = _cacheService.GetData<IEnumerable<CategoryDTO>>(key);
    if (cacheCategories is not null) return cacheCategories;
    // get categories from db
    IEnumerable<CategoryDTO> categories = await _context
        .Categories.AsNoTracking()
        .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
        .Where(c => !c.IsDeleted)
        .Select(category => category.MapToDTO())
        .ToListAsync(cancellationToken);
    // set categories to cache
    _cacheService.SetData(key, categories);
    return categories;
  }

  public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    //get category from cache
    string key = $"category-{id}";
    CategoryDTO? cacheCategory = _cacheService.GetData<CategoryDTO>(key);
    if (cacheCategory is not null) return cacheCategory;
    // get category from db
    CategoryDTO? category = await _context
      .Categories.AsNoTracking()
      .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
       .Where(c => !c.IsDeleted)
      .Where(c => c.Id.Equals(id))
      .Select(category => category.MapToDTO())
      .SingleOrDefaultAsync(cancellationToken);
    if (category is null) return null!;
    // set category to cache
    _cacheService.SetData(key, category);
    return category;
  }
}