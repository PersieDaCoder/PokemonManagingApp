using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
  public ReviewRepository(ApplicationDBContext context) : base(context)
  {
  }

  public async Task<IEnumerable<Review>> GetAllReviewsAsync(bool checkTraces)
  {
    IQueryable<Review> query = _dbSet.AsQueryable();
    query = checkTraces ? query.AsNoTracking() : query;
    return await query
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Reviewer).ThenInclude(r => r == null ? null! : r.Reviews)
      .Where(r => r.Status)
      .ToListAsync();
  }

  public async Task<Review?> GetReviewByIdAsync(Guid id, bool checkTraces)
  {
    IQueryable<Review> query = _dbSet.AsQueryable();
    query = checkTraces ? query.AsNoTracking() : query;
    return await query
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Reviewer).ThenInclude(r => r == null ? null! : r.Reviews)
      .Where(r => r.Id.Equals(id))
      .Where(r => r.Status)
      .SingleOrDefaultAsync();
  }
}