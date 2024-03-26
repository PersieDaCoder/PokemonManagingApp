using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Helpers;

namespace PokemonManagingApp.UseCases.Mapper;

public static class CategoryMapper
{
    public static CategoryDTO MapToDTO(this Category category)
    {
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            IsDeleted = category.IsDeleted,
            CreatedAt = category.CreatedAt,
            Pokemons = category.PokemonCategories is null ? [] :
                category.PokemonCategories
                    .Select(pc => pc.Pokemon)
                    .Select(p => p == null ? null! : new PokemonDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BirthDate = p.BirthDate,
                        IsDeleted = p.IsDeleted,
                        Owners = p.PokemonOwners is null ? [] :
                        p.PokemonOwners
                            .Where(pokemonOwner => pokemonOwner.PokemonId == p.Id)
                            .Select(pokemonOwner => pokemonOwner.Owner)
                            .Select(owner => owner == null ? null! : new OwnerDTO
                            {
                                Id = owner.Id,
                                UserName = owner.UserName ?? string.Empty,
                                IsDeleted = owner.IsDeleted,
                                CreatedAt = owner.CreatedAt,
                                Email = owner.Email,
                                Role = owner.Role.ConvertIntToString(),
                                Gym = owner.Gym is null ? null! : new GymDTO
                                {
                                    Id = owner.Gym.Id,
                                    Name = owner.Gym.Name,
                                    IsDeleted = owner.Gym.IsDeleted,
                                    CreatedAt = owner.Gym.CreatedAt,
                                },
                                Country = owner.Country is null ? null! : new CountryDTO
                                {
                                    Id = owner.Country.Id,
                                    Name = owner.Country.Name,
                                    IsDeleted = owner.Country.IsDeleted,
                                    CreatedAt = owner.Country.CreatedAt,
                                    Owners = owner.Country.Owners is null ? [] : owner.Country.Owners.Select(owner => owner == null ? null! : new OwnerDTO
                                    {
                                        Id = owner.Id,
                                        UserName = owner.UserName,
                                        IsDeleted = owner.IsDeleted,
                                        CreatedAt = owner.CreatedAt,
                                        Email = owner.Email,
                                        Role = owner.Role.ConvertIntToString(),
                                        Gym = owner.Gym is null ? null! : new GymDTO
                                        {
                                            Id = owner.Gym.Id,
                                            Name = owner.Gym.Name,
                                            IsDeleted = owner.Gym.IsDeleted,
                                            CreatedAt = owner.Gym.CreatedAt,
                                        },
                                        Reviews = owner.Reviews is null ? [] : owner.Reviews.Select(review => review == null ? null! : new ReviewDTO
                                        {
                                            Id = review.Id,
                                            Title = review.Title,
                                            Text = review.Text,
                                            IsDeleted = review.IsDeleted,
                                            CreatedAt = review.CreatedAt,
                                        }).ToList(),
                                    })
                                }
                            }).ToList(),
                        Reviews = p.Reviews is null ? [] :
                            p.Reviews.Select(review => new ReviewDTO
                            {
                                Id = review.Id,
                                Title = review.Title,
                                Text = review.Text,
                                IsDeleted = review.IsDeleted,
                                CreatedAt = review.CreatedAt,
                            }).ToList(),
                    }).ToList(),
        };
    }
}
