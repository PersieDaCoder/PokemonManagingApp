using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
  public Task<IEnumerable<Review>> GetAllReviewsAsync(CancellationToken cancellationToken = default);
  public Task<Review> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default);
}