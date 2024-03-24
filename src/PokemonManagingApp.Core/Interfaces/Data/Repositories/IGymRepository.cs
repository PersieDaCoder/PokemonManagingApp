using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IGymRepository : IBaseRepository<Gym>
{
    Task<Gym?> GetGymByIdAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IEnumerable<Gym>> GetGymsAsync(CancellationToken cancellationToken = default);
}