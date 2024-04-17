using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Helpers;

namespace PokemonManagingApp.UseCases.Mapper;

public static class ReviewMapper
{
    public static ReviewDTO MapToDTO(this Review review)
    {
        return new ReviewDTO
        {
            Id = review.Id,
            Text = review.Text,
            IsDeleted = review.IsDeleted,
            Title = review.Title,
            CreatedAt = review.CreatedAt,
            Owner = review.Owner is null ? null! : new OwnerDTO
            {
                Id = review.Owner.Id,
                UserName = review.Owner.UserName ?? string.Empty,
                IsDeleted = review.Owner.IsDeleted,
                Country = review.Owner.Country is null ? null! : new CountryDTO
                {
                    Id = review.Owner.Country.Id,
                    Name = review.Owner.Country.Name,
                    IsDeleted = review.Owner.Country.IsDeleted,
                    CreatedAt = review.Owner.Country.CreatedAt,
                },
                CreatedAt = review.Owner.CreatedAt,
                Email = review.Owner.Email,
                Role = review.Owner.Role.ConvertIntToString(),
                Reviews = review.Owner.Reviews is null ? [] : review.Owner.Reviews
                        .Select(review => new ReviewDTO
                        {
                            Id = review.Id,
                            Title = review.Title,
                            Text = review.Text,
                            IsDeleted = review.IsDeleted,
                            CreatedAt = review.CreatedAt,
                        }).ToList(),
                Gym = review.Owner.Gym is null ? null! : new GymDTO
                {
                    Id = review.Owner.Gym.Id,
                    Name = review.Owner.Gym.Name,
                    IsDeleted = review.Owner.Gym.IsDeleted,
                    CreatedAt = review.Owner.Gym.CreatedAt,
                },
                Pokemons = review.Owner.PokemonOwners is null ? [] : review.Owner.PokemonOwners
                        .Select(po => po.Pokemon)
                        .Select(pokemon => pokemon == null ? null! : new PokemonDTO
                        {
                            Id = pokemon.Id,
                            Name = pokemon.Name,
                            IsDeleted = pokemon.IsDeleted,
                            BirthDate = pokemon.BirthDate,
                            Height = pokemon.Height,
                            Weight = pokemon.Weight,
                            Description = pokemon.Description,
                            ImageUrl = pokemon.ImageUrl,
                            Categories = pokemon.PokemonCategories is null ? [] : pokemon.PokemonCategories
                                .Select(pc => pc.Category)
                                .Select(category => category == null ? null! : new CategoryDTO
                                {
                                    Id = category.Id,
                                    Name = category.Name,
                                    IsDeleted = category.IsDeleted,
                                    CreatedAt = category.CreatedAt,
                                }),
                        }),
            },
            Pokemon = review.Pokemon is null ? null! : new PokemonDTO
            {
                Id = review.Pokemon.Id,
                Name = review.Pokemon.Name,
                BirthDate = review.Pokemon.BirthDate,
                IsDeleted = review.Pokemon.IsDeleted,
                Height = review.Pokemon.Height,
                Weight = review.Pokemon.Weight,
                Description = review.Pokemon.Description,
                ImageUrl = review.Pokemon.ImageUrl,
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
                Owners = review.Pokemon.PokemonOwners is null ? [] :
                    review.Pokemon.PokemonOwners
                        .Where(pokemonOwner => pokemonOwner.PokemonId == review.Pokemon.Id)
                        .Select(pokemonOwner => pokemonOwner.Owner)
                        .Select(owner => owner == null ? null! : new OwnerDTO
                        {
                            Id = owner.Id,
                            UserName = owner.UserName ?? string.Empty,
                            IsDeleted = owner.IsDeleted,
                            CreatedAt = owner.CreatedAt,
                            Email = owner.Email,
                            Role = owner.Role.ConvertIntToString(),
                            Reviews = owner.Reviews is null ? [] : owner.Reviews.Select(review => new ReviewDTO
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
                            Pokemons = owner.PokemonOwners is null ? [] :
                                owner.PokemonOwners
                                .Select(po => po.Pokemon)
                                .Select(pokemon => pokemon == null ? null! : new PokemonDTO
                                {
                                    Id = pokemon.Id,
                                    Name = pokemon.Name,
                                    IsDeleted = pokemon.IsDeleted,
                                    BirthDate = pokemon.BirthDate,
                                    Height = pokemon.Height,
                                    Weight = pokemon.Weight,
                                    Description = pokemon.Description,
                                    ImageUrl = pokemon.ImageUrl,
                                    Categories = pokemon.PokemonCategories is null ? [] :
                                        pokemon.PokemonCategories
                                        .Select(pc => pc.Category)
                                        .Select(category => category == null ? null! : new CategoryDTO
                                        {
                                            Id = category.Id,
                                            Name = category.Name,
                                            IsDeleted = category.IsDeleted,
                                            CreatedAt = category.CreatedAt,
                                        }),
                                }),
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
                            }
                        }).ToList(),
            },
        };
    }
}