using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class GymRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Gym>(context, cacheService), IGymRepository
{
    public async Task<Gym?> GetGymByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        //try to get gym from cache
        string key = $"gym-{id}";
        Gym? cachedGym = _cacheService.GetData<Gym>(key);
        if (cachedGym is not null) return cachedGym;
        //if not found in cache, get gym from database
        Gym? gym = await _dbSet.AsNoTracking().SingleOrDefaultAsync(g => g.Id.Equals(id), cancellationToken);
        if (gym is null) return null;
        //set gym to cache
        _cacheService.SetData<Gym>(key, gym);
        return gym;
    }

    public async Task<IEnumerable<Gym>> GetGymsAsync(CancellationToken cancellationToken = default)
    {
        //try to get gyms from cache
        string key = "gyms-all";
        IEnumerable<Gym>? cachedGyms = _cacheService.GetData<IEnumerable<Gym>>(key);
        if (cachedGyms is not null) return cachedGyms;
        //if not found in cache, get gyms from database
        IEnumerable<Gym> gyms = await _dbSet.AsNoTracking().ToListAsync();
        //set gyms to cache
        _cacheService.SetData<IEnumerable<Gym>>(key, gyms);
        return gyms;
    }
}