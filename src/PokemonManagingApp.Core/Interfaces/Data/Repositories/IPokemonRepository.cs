using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<Pokemon?> GetPokemonByIdAsync(Guid id, bool checkTraces);
    Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(bool checkTraces);
}