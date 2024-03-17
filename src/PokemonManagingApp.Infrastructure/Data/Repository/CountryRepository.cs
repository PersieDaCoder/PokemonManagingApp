using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class CountryRepository : BaseRepository<Country>, ICountryRepository
{
  public CountryRepository(ApplicationDBContext context) : base(context)
  {
  }

  public async Task<IEnumerable<Country>> GetAllCountries(bool trackChanges)
  {
    IQueryable<Country> query = _dbSet.AsQueryable();
    query = trackChanges ? query : query.AsNoTracking();
    return await query
      .Include(c => c.Owners)
      .Where(c => c.Status)
      .ToListAsync();
  }

  public async Task<Country?> GetCountryById(Guid id, bool trackChanges)
  {
    IQueryable<Country> query = _dbSet.AsQueryable();
    query = trackChanges ? query : query.AsNoTracking();
    return await query
      .Include(c => c.Owners)
      .Where(c => c.Status)
      .SingleOrDefaultAsync();
  }
}