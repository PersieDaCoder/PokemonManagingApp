using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(ApplicationDBContext context) : base(context)
    {
    }


    public async Task<IEnumerable<Owner>> GetAllOwners(bool checkTraces)
    {
        IQueryable<Owner> query = _dbSet.AsQueryable();
        query = checkTraces ? query : query.AsNoTracking();
        return await query
            .Include(o => o.PokemonOwners).ThenInclude(po => po.Pokemon)
            .Include(o => o.Country)
            .Where(o => o.Status)
            .ToListAsync();
    }

    public Task<Owner?> GetOwnerById(Guid id, bool checkTraces)
    {
        IQueryable<Owner> query = _dbSet.AsQueryable();
        query = checkTraces ? query : query.AsNoTracking();
        return query
            .Include(o => o.PokemonOwners).ThenInclude(po => po.Pokemon)
            .Include(o => o.Country)
            .FirstOrDefaultAsync(o => o.Id.Equals(id));
    }
}