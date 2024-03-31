using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<IEnumerable<PokemonDTO>> GetAllPokemonsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PokemonDTO>> GetPokemonsByOwnerIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<PokemonDTO> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PokemonDTO>> GetPokemonsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
}