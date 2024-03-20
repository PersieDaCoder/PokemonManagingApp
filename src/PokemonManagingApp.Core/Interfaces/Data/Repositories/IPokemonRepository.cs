using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(CancellationToken cancellationToken = default);
    Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken = default);
}