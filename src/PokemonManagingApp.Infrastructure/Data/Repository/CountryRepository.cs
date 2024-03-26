using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CountryRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Country>(context, cacheService), ICountryRepository
{
  public async Task<IEnumerable<Country>> GetAllCountries(CancellationToken cancellationToken = default)
  {
    //get countries from cache
    string key = $"countries-all";
    IEnumerable<Country>? cacheCountries = _cacheService.GetData<IEnumerable<Country>>(key);
    if (cacheCountries is not null) return cacheCountries;
    // get countries from db
    IEnumerable<Country> countries = await _context
        .Countries.AsNoTracking()
        .Include(country => country.Owners).ThenInclude(owner => owner.PokemonOwners).ThenInclude(pokemonOwner => pokemonOwner.Pokemon)
        .Where(pokemon => !pokemon.IsDeleted)
        .ToListAsync(cancellationToken);
    // set countries to cache
    _cacheService.SetData(key, countries);
    return countries;
  }

  public async Task<Country> GetCountryById(Guid id, CancellationToken cancellationToken = default)
  {
    // get country from cache
    string key = $"countries-{id}";
    Country? cachedCountry = _cacheService.GetData<Country>(key);
    if (cachedCountry is not null) return cachedCountry;
    // get country from db
    Country? country = await _context
      .Countries.AsNoTracking()
      .Include(c => c.Owners).ThenInclude(o => o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(c => !c.IsDeleted)
      .SingleOrDefaultAsync(cancellationToken);
    if (country is null) return null!;
    // set country to cache
    _cacheService.SetData(key, country);
    return country;
  }
}