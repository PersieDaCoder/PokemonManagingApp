using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.Mapper;

public static class GymMapper
{
    public static GymDTO MapToDTO(this Gym gym)
    {
        return new GymDTO
        {
            Id = gym.Id,
            Name = gym.Name,
            CreatedAt = gym.CreatedAt,
            IsDeleted = gym.IsDeleted,
        };
    }
}