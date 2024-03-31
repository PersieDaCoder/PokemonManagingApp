using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class OwnerRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Owner>(context, cacheService), IOwnerRepository
{
    public async Task<IEnumerable<OwnerDTO>> GetAllOwners(CancellationToken cancellationToken = default)
    {
        // get owners from cache
        string key = "owners-all";
        IEnumerable<OwnerDTO>? cachedOwners = _cacheService.GetData<IEnumerable<OwnerDTO>>(key);
        if (cachedOwners is not null) return cachedOwners;
        // get owners from database
        IEnumerable<OwnerDTO> owners = await _context
            .Owners.AsNoTracking()
            .Include(owner => owner.PokemonOwners).ThenInclude(pokemonOwner => pokemonOwner.Pokemon)
            .Include(owner => owner.Country)
            .Include(owner => owner.Gym)
            .Where(owner => !owner.IsDeleted)
            .Select(owner => owner.MapToDTO())
            .ToListAsync(cancellationToken);
        // set owners to cache
        _cacheService.SetData(key, owners);
        return owners;
    }

    public async Task<OwnerDTO> GetOwnerById(Guid id, CancellationToken cancellationToken = default)
    {
        // get owner from cache
        string key = $"owner-{id}";
        OwnerDTO? cachedOwner = _cacheService.GetData<OwnerDTO>(key);
        if (cachedOwner is not null) return cachedOwner;
        // get owner from database
        OwnerDTO? owner = await _context
            .Owners
            .AsNoTracking()
            .Include(owner => owner.PokemonOwners).ThenInclude(pokemonOwner => pokemonOwner.Pokemon)
            .Include(owner => owner.Country)
            .Include(owner => owner.Gym)
            .Where(owner => !owner.IsDeleted)
            .Select(owner => owner.MapToDTO())
            .FirstOrDefaultAsync(o => o.Id.Equals(id), cancellationToken);
        if (owner is null) return null!;
        // set owner to cache
        _cacheService.SetData(key, owner);
        return owner;
    }
}