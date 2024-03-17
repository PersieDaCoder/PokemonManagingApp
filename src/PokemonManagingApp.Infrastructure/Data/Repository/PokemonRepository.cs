using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;

using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class PokemonRepository : BaseRepository<Pokemon>, IPokemonRepository
{
  public PokemonRepository(ApplicationDBContext context) : base(context)
  {
  }


  public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(bool checkTraces)
  {
    IQueryable<Pokemon> query = _dbSet.AsQueryable();
    query = checkTraces ? query : query.AsNoTracking();
    return await query
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
      .Include(p => p.Reviews).ThenInclude(r => r.Reviewer)
      .ToListAsync();
  }

  public async Task<Pokemon?> GetPokemonByIdAsync(Guid id, bool checkTraces)
  {
    IQueryable<Pokemon> query = _dbSet.AsQueryable();
    query = checkTraces ? query : query.AsNoTracking();
    return await query
      .Include(p => p.PokemonCategories).ThenInclude(pc => pc.Category)
      .Include(p => p.PokemonOwners).ThenInclude(po => po.Owner)
      .Include(p => p.Reviews).ThenInclude(r => r.Reviewer)
      .FirstOrDefaultAsync(p => p.Id.Equals(id));
  }
  /**/
}