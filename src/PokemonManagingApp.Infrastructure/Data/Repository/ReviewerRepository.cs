using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class ReviewerRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Reviewer>(context, cacheService), IReviewerRepository
{
  public async Task<IEnumerable<Reviewer>> GetAllReviewersAsync(CancellationToken cancellationToken = default)
  {
    string key = "reviewers-all";
    IEnumerable<Reviewer>? cachedReviewers = _cacheService.GetData<IEnumerable<Reviewer>>(key);
    if (cachedReviewers is not null) return cachedReviewers;
    // get reviewers from database
    IEnumerable<Reviewer> reviewers = await _context
      .Reviewers.AsNoTracking()
      .Include(r => r.Reviews).ThenInclude(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Reviews).ThenInclude(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonOwners).ThenInclude(pc => pc.Owner)
      .Where(r => r.Status)
      .ToListAsync(cancellationToken);
    // set reviewers to cache
    var expireTime = TimeSpan.FromMinutes(2);
    _cacheService.SetData(key, reviewers, expireTime);
    return reviewers;
  }

  public async Task<Reviewer?> GetReviewerByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    // get reviewer from cache
    string key = $"reviewer-{id}";
    Reviewer? cachedReviewer = _cacheService.GetData<Reviewer>(key);
    if (cachedReviewer is not null) return cachedReviewer;
    // get reviewer from db
    Reviewer? reviewer = await _context
      .Reviewers.AsNoTracking()
      .Include(r => r.Reviews).ThenInclude(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Reviews).ThenInclude(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonOwners).ThenInclude(pc => pc.Owner)
      .Where(r => r.Id.Equals(id))
      .Where(r => r.Status)
      .SingleOrDefaultAsync(cancellationToken);
    if (reviewer is null) return null!;
    // set reviewer to cache
    var expireTime = TimeSpan.FromMinutes(2);
    _cacheService.SetData(key, reviewer, expireTime);
    return reviewer;
  }
}