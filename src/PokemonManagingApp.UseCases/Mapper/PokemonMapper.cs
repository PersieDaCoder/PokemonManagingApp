
using System.Runtime.Intrinsics.X86;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Helpers;

namespace PokemonManagingApp.UseCases;

public static class PokemonMapper
{
    public static PokemonDTO MapToDTO(this Pokemon pokemon)
        => new PokemonDTO
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            BirthDate = pokemon.BirthDate,
            IsDeleted = pokemon.IsDeleted,
            Description = pokemon.Description,
            ImageUrl = pokemon.ImageUrl,
            Height = pokemon.Height,
            Weight = pokemon.Weight,
            Categories = pokemon.PokemonCategories is null ? [] :
                pokemon.PokemonCategories
                .Select(pc => pc.Category)
                .Select(c => c == null ? null! : new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsDeleted = c.IsDeleted,
                    CreatedAt = c.CreatedAt,
                }),
            Owners = pokemon.PokemonOwners is null ? [] :
                pokemon.PokemonOwners
                    .Where(pokemonOwner => pokemonOwner.PokemonId == pokemon.Id)
                    .Select(pokemonOwner => pokemonOwner.Owner)
                    .Select(owner => owner == null ? null! : new OwnerDTO
                    {
                        Id = owner.Id,
                        UserName = owner.UserName ?? string.Empty,
                        IsDeleted = owner.IsDeleted,
                        Country = owner.Country is null ? null! : new CountryDTO
                        {
                            Id = owner.Country.Id,
                            Name = owner.Country.Name,
                            IsDeleted = owner.Country.IsDeleted,
                            CreatedAt = owner.Country.CreatedAt,
                        },
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
                        Reviews = owner.Reviews is null ? [] :
                            owner.Reviews.Select(review => review == null ? null! : new ReviewDTO
                            {
                                Id = review.Id,
                                Title = review.Title,
                                Text = review.Text,
                                IsDeleted = review.IsDeleted,
                                CreatedAt = review.CreatedAt,
                            }).ToList(),
                    }).ToList(),
            Reviews = pokemon.Reviews is null ? [] :
                pokemon.Reviews.Select(review => new ReviewDTO
                {
                    Id = review.Id,
                    Title = review.Title,
                    Text = review.Text,
                    IsDeleted = review.IsDeleted,
                    CreatedAt = review.CreatedAt,
                    Pokemon = review.Pokemon is null ? null! : new PokemonDTO
                    {
                        Id = review.Pokemon.Id,
                        Name = review.Pokemon.Name,
                        BirthDate = review.Pokemon.BirthDate,
                        IsDeleted = review.Pokemon.IsDeleted,
                        Description = review.Pokemon.Description,
                        ImageUrl = review.Pokemon.ImageUrl,
                        Height = review.Pokemon.Height,
                        Weight = review.Pokemon.Weight,
                        Categories = review.Pokemon.PokemonCategories is null ? [] :
                            review.Pokemon.PokemonCategories
                            .Select(pc => pc.Category)
                            .Select(c => c == null ? null! : new CategoryDTO
                            {
                                Id = c.Id,
                                Name = c.Name,
                                IsDeleted = c.IsDeleted,
                                CreatedAt = c.CreatedAt,
                            }),
                    },
                }).ToList(),
        };
}
