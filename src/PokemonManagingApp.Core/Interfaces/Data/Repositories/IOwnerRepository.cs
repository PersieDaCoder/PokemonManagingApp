using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Task<IEnumerable<Owner>> GetAllOwners(bool checkTraces);
    Task<Owner?> GetOwnerById(Guid id, bool checkTraces);
}