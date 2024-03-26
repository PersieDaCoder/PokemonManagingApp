using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class ReviewRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Review>(context, cacheService), IReviewRepository
{
  public async Task<IEnumerable<Review>> GetReviewsAsync(CancellationToken cancellationToken = default)
  {
    // get reviews from cache
    string key = "reviews-all";
    IEnumerable<Review>? cachedReviews = _cacheService.GetData<IEnumerable<Review>>(key);
    if (cachedReviews is not null) return cachedReviews;
    // get reviews from database
    IEnumerable<Review> reviews = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Owner).ThenInclude(o => o == null ? null! : o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(r => !r.IsDeleted)
      .ToListAsync(cancellationToken);
    // set reviews to cache
    _cacheService.SetData(key, reviews);
    return reviews;
  }

  public async Task<IEnumerable<Review>> GetReviewsByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
  {
    string key = $"reviews-owner-{ownerId}";
    // get reviews from cache
    IEnumerable<Review>? cachedReviews = _cacheService.GetData<IEnumerable<Review>>(key);
    if (cachedReviews is not null) return cachedReviews;
    // get reviews from database
    IEnumerable<Review> reviews = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Owner).ThenInclude(o => o == null ? null! : o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(r => r.OwnerId.Equals(ownerId))
      .Where(r => !r.IsDeleted)
      .ToListAsync(cancellationToken);
    // set reviews to cache
    _cacheService.SetData(key, reviews);
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
      .Include(r => r.Owner).ThenInclude(o => o == null ? null! : o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(r => r.Id.Equals(id))
      .Where(r => !r.IsDeleted)
      .SingleOrDefaultAsync(cancellationToken);
    if (review is null) return null!;
    // set review to cache
    _cacheService.SetData(key, review);
    return review;
  }

  public async Task<IEnumerable<Review>> GetReviewsByPokemonIdAsync(Guid pokemonId, CancellationToken cancellationToken = default)
  {
    string key = $"reviews-pokemon-{pokemonId}";
    // get reviews from cache
    IEnumerable<Review>? cachedReviews = _cacheService.GetData<IEnumerable<Review>>(key);
    if (cachedReviews is not null) return cachedReviews;
    // get reviews from database
    IEnumerable<Review> reviews = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Owner).ThenInclude(o => o == null ? null! : o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(r => r.PokemonId.Equals(pokemonId))
      .Where(r => !r.IsDeleted)
      .ToListAsync(cancellationToken);
    // set reviews to cache
    _cacheService.SetData(key, reviews);
    return reviews;
  }
}