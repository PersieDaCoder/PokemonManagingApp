using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
  // No Tracking Methods
  Task<IEnumerable<Review>> GetReviewsAsync(CancellationToken cancellationToken = default);
  Task<IEnumerable<Review>> GetReviewsByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
  Task<IEnumerable<Review>> GetReviewsByPokemonIdAsync(Guid pokemonId, CancellationToken cancellationToken = default);
  Task<Review> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default);
}