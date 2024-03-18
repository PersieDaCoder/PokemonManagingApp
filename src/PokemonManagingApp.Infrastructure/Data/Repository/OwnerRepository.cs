using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class OwnerRepository(ApplicationDBContext context, IMemoryCache memoryCache) : BaseRepository<Owner>(context), IOwnerRepository
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<IEnumerable<Owner>> GetAllOwners(bool checkTraces)
    {
        string key = "owners-all";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            IQueryable<Owner> query = _dbSet.AsQueryable();
            query = checkTraces ? query : query.AsNoTracking();
            return await query
                .Include(o => o.PokemonOwners).ThenInclude(po => po.Pokemon)
                .Include(o => o.Country)
                .Where(o => o.Status)
                .ToListAsync();
        }) ?? [];
    }

    public Task<Owner?> GetOwnerById(Guid id, bool checkTraces)
    {
        string key = $"owners-{id}";
        return _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            IQueryable<Owner> query = _dbSet.AsQueryable();
            query = checkTraces ? query : query.AsNoTracking();
            return await query
                .Include(o => o.PokemonOwners).ThenInclude(po => po.Pokemon)
                .Include(o => o.Country)
                .FirstOrDefaultAsync(o => o.Id.Equals(id));
        });
    }
}