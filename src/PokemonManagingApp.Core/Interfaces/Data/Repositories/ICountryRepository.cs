using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Core.Interfaces.Data.Repositories;

public interface ICountryRepository : IBaseRepository<Country>
{
    Task<IEnumerable<Country>> GetAllCountries(CancellationToken cancellationToken = default);
    Task<Country> GetCountryById(Guid id, CancellationToken cancellationToken = default);
}