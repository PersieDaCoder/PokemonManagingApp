using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IGymRepository : IBaseRepository<Gym>
{
    Task<GymDTO?> GetGymByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<GymDTO>> GetGymsAsync(CancellationToken cancellationToken = default);
}