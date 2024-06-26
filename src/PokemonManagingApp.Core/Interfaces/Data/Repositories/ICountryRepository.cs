using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface ICountryRepository : IBaseRepository<Country>
{
    Task<IEnumerable<CountryDTO>> GetAllCountriesAsync(CancellationToken cancellationToken = default);
    Task<CountryDTO?> GetCountryByIdAsync(Guid id, CancellationToken cancellationToken = default);
}