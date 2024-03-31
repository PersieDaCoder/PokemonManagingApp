using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CountryRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Country>(context, cacheService), ICountryRepository
{
  public async Task<IEnumerable<CountryDTO>> GetAllCountriesAsync(CancellationToken cancellationToken = default)
  {
    //get countries from cache
    string key = $"countries-all";
    IEnumerable<CountryDTO>? cacheCountries = _cacheService.GetData<IEnumerable<CountryDTO>>(key);
    if (cacheCountries is not null) return cacheCountries;
    // get countries from db
    IEnumerable<CountryDTO> countries = await _context
        .Countries.AsNoTracking()
        .Include(country => country.Owners).ThenInclude(owner => owner.PokemonOwners).ThenInclude(pokemonOwner => pokemonOwner.Pokemon)
        .Where(country => !country.IsDeleted)
        .Select(country => country.MapToDTO())
        .ToListAsync(cancellationToken);
    // set countries to cache
    _cacheService.SetData(key, countries);
    return countries;
  }

  public async Task<CountryDTO?> GetCountryByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    // get country from cache
    string key = $"countries-{id}";
    CountryDTO? cachedCountry = _cacheService.GetData<CountryDTO>(key);
    if (cachedCountry is not null) return cachedCountry;
    // get country from db
    CountryDTO? country = await _context
      .Countries.AsNoTracking()
      .Include(c => c.Owners).ThenInclude(o => o.PokemonOwners).ThenInclude(po => po.Pokemon)
      .Where(c => !c.IsDeleted)
      .Select(country => country.MapToDTO())
      .SingleOrDefaultAsync(cancellationToken);
    if (country is null) return null!;
    // set country to cache
    _cacheService.SetData(key, country);
    return country;
  }
}