using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.DTOs.Pokemon;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<IEnumerable<PokemonDTO>> GetAllDTOs();
}