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
            DeletedAt = category.DeletedAt,
            Pokemons = category.PokemonCategories is null ? [] :
                category.PokemonCategories
                    .Select(pc => pc.Pokemon)
                    .Select(p => p == null ? null! : new PokemonDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BirthDate = p.BirthDate,
                        IsDeleted = p.IsDeleted,
                        DeletedAt = p.DeletedAt,
                        CreatedAt = p.CreatedAt,
                        Owners = p.PokemonOwners is null ? [] :
                        p.PokemonOwners
                            .Where(pokemonOwner => pokemonOwner.PokemonId == p.Id)
                            .Select(pokemonOwner => pokemonOwner.Owner)
                            .Select(owner => owner == null ? null! : new OwnerDTO
                            {
                                Id = owner.Id,
                                UserName = owner.UserName ?? string.Empty,
                                IsDeleted = owner.IsDeleted,
                                DeletedAt = owner.DeletedAt,
                                CreatedAt = owner.CreatedAt,
                                Email = owner.Email,
                                Role = owner.Role.ConvertIntToString(),
                                Gym = owner.Gym is null ? null! : new GymDTO
                                {
                                    Id = owner.Gym.Id,
                                    Name = owner.Gym.Name,
                                    CreatedAt = owner.Gym.CreatedAt,
                                    IsDeleted = owner.Gym.IsDeleted,
                                    DeletedAt = owner.Gym.DeletedAt,
                                },
                                Country = owner.Country is null ? null! : new CountryDTO
                                {
                                    Id = owner.Country.Id,
                                    Name = owner.Country.Name,
                                    CreatedAt = owner.Country.CreatedAt,
                                    IsDeleted = owner.Country.IsDeleted,
                                    DeletedAt = owner.Country.DeletedAt,
                                    Owners = owner.Country.Owners is null ? [] : owner.Country.Owners.Select(owner => owner == null ? null! : new OwnerDTO
                                    {
                                        Id = owner.Id,
                                        UserName = owner.UserName,
                                        Email = owner.Email,
                                        CreatedAt = owner.CreatedAt,
                                        IsDeleted = owner.IsDeleted,
                                        DeletedAt = owner.DeletedAt,
                                        Role = owner.Role.ConvertIntToString(),
                                        Gym = owner.Gym is null ? null! : new GymDTO
                                        {
                                            Id = owner.Gym.Id,
                                            Name = owner.Gym.Name,
                                            CreatedAt = owner.Gym.CreatedAt,
                                            IsDeleted = owner.Gym.IsDeleted,
                                            DeletedAt = owner.Gym.DeletedAt,
                                        },
                                        Reviews = owner.Reviews is null ? [] : owner.Reviews.Select(review => review == null ? null! : new ReviewDTO
                                        {
                                            Id = review.Id,
                                            Title = review.Title,
                                            Text = review.Text,
                                            IsDeleted = review.IsDeleted,
                                            CreatedAt = review.CreatedAt,
                                            DeletedAt = review.DeletedAt,
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
                                DeletedAt = review.DeletedAt,
                            }).ToList(),
                    }).ToList(),
        };
    }
}
