using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class ReviewRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Review>(context, cacheService), IReviewRepository
{
  public async Task<IEnumerable<Review>> GetAllReviewsAsync(CancellationToken cancellationToken = default)
  {
    // get reviews from cache
    string key = "reviews-all";
    IEnumerable<Review>? cachedReviews = _cacheService.GetData<IEnumerable<Review>>(key);
    if (cachedReviews is not null) return cachedReviews;
    // get reviews from database
    IEnumerable<Review> reviews = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Where(r => r.Status)
      .ToListAsync(cancellationToken);
    // set reviews to cache
    var expireTime = TimeSpan.FromMinutes(2);
    _cacheService.SetData(key, reviews, expireTime);
    return reviews;
  }

  public async Task<Review> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    string key = $"review-{id}";
    // get review from cache
    Review? cachedReview = _cacheService.GetData<Review>(key);
    if (cachedReview is not null) return cachedReview;
    // get review from database
    Review? review = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Where(r => r.Id.Equals(id))
      .Where(r => r.Status)
      .SingleOrDefaultAsync(cancellationToken);
    if (review is null) return null!;
    // set review to cache
    var expireTime = TimeSpan.FromMinutes(2);
    _cacheService.SetData(key, review, expireTime);
    return review;
  }
}