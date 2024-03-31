using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
  // No Tracking Methods
  Task<IEnumerable<ReviewDTO>> GetReviewsAsync(CancellationToken cancellationToken = default);
  Task<IEnumerable<ReviewDTO>> GetReviewsByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
  Task<IEnumerable<ReviewDTO>> GetReviewsByPokemonIdAsync(Guid pokemonId, CancellationToken cancellationToken = default);
  Task<ReviewDTO?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default);
}