using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<IEnumerable<Pokemon>> GetAll();
}