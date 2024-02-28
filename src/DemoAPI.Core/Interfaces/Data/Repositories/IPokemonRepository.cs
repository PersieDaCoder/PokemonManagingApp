using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Core.DTOs.Pokemon;
using DemoAPI.Core.Models;

namespace DemoAPI.Core.Interfaces.Data.Repositories;

public interface IPokemonRepository : IBaseRepository<Pokemon>
{
    Task<IEnumerable<PokemonDTO>> GetAllDTOs();
}