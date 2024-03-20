using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class OwnerRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Owner>(context, cacheService), IOwnerRepository
{
    public async Task<IEnumerable<Owner>> GetAllOwners(CancellationToken cancellationToken = default)
    {
        // get owners from cache
        string key = "owners-all";
        IEnumerable<Owner>? cachedOwners = _cacheService.GetData<IEnumerable<Owner>>(key);
        if (cachedOwners is not null) return cachedOwners;
        // get owners from database
        IEnumerable<Owner> owners = await _context
            .Owners.AsNoTracking()
            .Include(o => o.PokemonOwners).ThenInclude(po => po.Pokemon)
            .Include(o => o.Country)
            .Where(o => o.Status)
            .ToListAsync(cancellationToken);
        // set owners to cache
        var expireTime = TimeSpan.FromMinutes(2);
        _cacheService.SetData(key, owners, expireTime);
        return owners;
    }

    public async Task<Owner> GetOwnerById(Guid id, CancellationToken cancellationToken = default)
    {
        // get owner from cache
        string key = $"owner-{id}";
        Owner? cachedOwner = _cacheService.GetData<Owner>(key);
        if (cachedOwner is not null) return cachedOwner;
        // get owner from database
        Owner? owner = await _context
            .Owners
            .AsNoTracking()
            .Include(o => o.PokemonOwners).ThenInclude(po => po.Pokemon)
            .Include(o => o.Country)
            .FirstOrDefaultAsync(o => o.Id.Equals(id), cancellationToken);
        if (owner is null) return null!;
        // set owner to cache
        var expireTime = TimeSpan.FromMinutes(2);
        _cacheService.SetData(key, owner, expireTime);
        return owner;
    }
}