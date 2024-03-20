using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Task<IEnumerable<Owner>> GetAllOwners(CancellationToken cancellationToken = default);
    Task<Owner> GetOwnerById(Guid id, CancellationToken cancellationToken = default);
}