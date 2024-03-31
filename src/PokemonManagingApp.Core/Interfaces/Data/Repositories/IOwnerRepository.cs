using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Task<IEnumerable<OwnerDTO>> GetAllOwners(CancellationToken cancellationToken = default);
    Task<OwnerDTO> GetOwnerById(Guid id, CancellationToken cancellationToken = default);
}