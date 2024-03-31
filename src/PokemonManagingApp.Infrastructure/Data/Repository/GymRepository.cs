using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.Core.Interfaces.Caching;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class GymRepository(ApplicationDBContext context, ICacheService cacheService) : BaseRepository<Gym>(context, cacheService), IGymRepository
{
    public async Task<GymDTO?> GetGymByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        //try to get gym from cache
        string key = $"gym-{id}";
        GymDTO? cachedGym = _cacheService.GetData<GymDTO>(key);
        if (cachedGym is not null) return cachedGym;
        //if not found in cache, get gym from database
        GymDTO? gym = await _dbSet.AsNoTracking()
        .Where(gym => !gym.IsDeleted)
        .Select(gym => gym.MapToDTO())
        .SingleOrDefaultAsync(g => g.Id.Equals(id), cancellationToken);
        if (gym is null) return null;
        //set gym to cache
        _cacheService.SetData<GymDTO>(key, gym);
        return gym;
    }

    public async Task<IEnumerable<GymDTO>> GetGymsAsync(CancellationToken cancellationToken = default)
    {
        //try to get gyms from cache
        string key = "gyms-all";
        IEnumerable<GymDTO>? cachedGyms = _cacheService.GetData<IEnumerable<GymDTO>>(key);
        if (cachedGyms is not null) return cachedGyms;
        //if not found in cache, get gyms from database
        IEnumerable<GymDTO> gyms = await _dbSet.AsNoTracking()
        .Where(gym => !gym.IsDeleted)
        .Select(gym => gym.MapToDTO())
        .ToListAsync();
        //set gyms to cache
        _cacheService.SetData<IEnumerable<GymDTO>>(key, gyms);
        return gyms;
    }
}