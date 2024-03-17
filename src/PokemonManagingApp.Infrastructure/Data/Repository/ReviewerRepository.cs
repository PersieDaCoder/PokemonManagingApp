using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class ReviewerRepository : BaseRepository<Reviewer>, IReviewerRepository
{
  public ReviewerRepository(ApplicationDBContext context) : base(context)
  {
  }

  public async Task<IEnumerable<Reviewer>> GetAllReviewersAsync(bool checkTraces)
  {
    IQueryable<Reviewer> query = _dbSet.AsQueryable();
    query = checkTraces ? query : query.AsNoTracking();
    return await query
      .Include(r => r.Reviews)
      .Where(r => r.Status)
      .ToListAsync();
  }

  public async Task<Reviewer?> GetReviewerByIdAsync(Guid id, bool checkTraces)
  {
    IQueryable<Reviewer> query = _dbSet.AsQueryable();
    query = checkTraces ? query : query.AsNoTracking();
    return await query
      .Include(r => r.Reviews)
      .Where(r => r.Id.Equals(id))
      .Where(r => r.Status)
      .SingleOrDefaultAsync();
  }
}