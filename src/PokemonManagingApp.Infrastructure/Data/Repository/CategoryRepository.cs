using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
  public CategoryRepository(ApplicationDBContext context) : base(context)
  {
  }


  public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    => await _dbSet.AsNoTracking()
      .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
      .Where(c => c.Status == true)
      .ToListAsync();
}