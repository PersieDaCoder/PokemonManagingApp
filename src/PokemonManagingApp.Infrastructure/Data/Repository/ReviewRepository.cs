using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class ReviewRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Review>(context, cacheService), IReviewRepository
{
  public async Task<IEnumerable<ReviewDTO>> GetReviewsAsync(CancellationToken cancellationToken = default)
  {
    // get reviews from cache
    string key = "reviews-all";
    IEnumerable<ReviewDTO>? cachedReviews = _cacheService.GetData<IEnumerable<ReviewDTO>>(key);
    if (cachedReviews is not null) return cachedReviews;
    // get reviews from database
    IEnumerable<ReviewDTO> reviews = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Owner).ThenInclude(o => o == null ? null! : o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(r => !r.IsDeleted)
      .Select(review => review.MapToDTO())
      .ToListAsync(cancellationToken);
    // set reviews to cache
    _cacheService.SetData(key, reviews);
    return reviews;
  }

  public async Task<IEnumerable<ReviewDTO>> GetReviewsByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
  {
    string key = $"reviews-owner-{ownerId}";
    // get reviews from cache
    IEnumerable<ReviewDTO>? cachedReviews = _cacheService.GetData<IEnumerable<ReviewDTO>>(key);
    if (cachedReviews is not null) return cachedReviews;
    // get reviews from database
    IEnumerable<ReviewDTO> reviews = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Owner).ThenInclude(o => o == null ? null! : o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(r => r.OwnerId.Equals(ownerId))
      .Where(r => !r.IsDeleted)
      .Select(review => review.MapToDTO())
      .ToListAsync(cancellationToken);
    // set reviews to cache
    _cacheService.SetData(key, reviews);
    return reviews;
  }

  public async Task<ReviewDTO?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    string key = $"review-{id}";
    // get review from cache
    ReviewDTO? cachedReview = _cacheService.GetData<ReviewDTO>(key);
    if (cachedReview is not null) return cachedReview;
    // get review from database
    ReviewDTO? review = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Owner).ThenInclude(o => o == null ? null! : o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(r => r.Id.Equals(id))
      .Where(r => !r.IsDeleted)
      .Select(review => review.MapToDTO())
      .SingleOrDefaultAsync(cancellationToken);
    if (review is null) return null!;
    // set review to cache
    _cacheService.SetData(key, review);
    return review;
  }

  public async Task<IEnumerable<ReviewDTO>> GetReviewsByPokemonIdAsync(Guid pokemonId, CancellationToken cancellationToken = default)
  {
    string key = $"reviews-pokemon-{pokemonId}";
    // get reviews from cache
    IEnumerable<ReviewDTO>? cachedReviews = _cacheService.GetData<IEnumerable<ReviewDTO>>(key);
    if (cachedReviews is not null) return cachedReviews;
    // get reviews from database
    IEnumerable<ReviewDTO> reviews = await _context
      .Reviews.AsNoTracking()
      .Include(r => r.Pokemon).ThenInclude(p => p == null ? null! : p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(r => r.Owner).ThenInclude(o => o == null ? null! : o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(r => r.PokemonId.Equals(pokemonId))
      .Where(r => !r.IsDeleted)
      .Select(review => review.MapToDTO())
      .ToListAsync(cancellationToken);
    // set reviews to cache
    _cacheService.SetData(key, reviews);
    return reviews;
  }
}