
using System.Runtime.Intrinsics.X86;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.DTOs;
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
            Status = pokemon.Status,
            Categories = pokemon.PokemonCategories is null ? [] :
                pokemon.PokemonCategories
                .Select(pc => pc.Category)
                .Select(c => c == null ? null! : new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Status = c.Status,
                }),
            Owners = pokemon.PokemonOwners is null ? [] :
                pokemon.PokemonOwners
                    .Where(pokemonOwner => pokemonOwner.PokemonId == pokemon.Id)
                    .Select(pokemonOwner => pokemonOwner.Owner)
                    .Select(owner => owner == null ? null! : new OwnerDTO
                    {
                        Id = owner.Id,
                        UserName = owner.UserName ?? string.Empty,
                        Status = owner.Status,
                        Country = owner.Country is null ? null! : new CountryDTO
                        {
                            Id = owner.Country.Id,
                            Name = owner.Country.Name,
                            Status = owner.Country.Status,
                            CreatedAt = owner.Country.CreatedAt,
                        },
                        CreatedAt = owner.CreatedAt,
                        Email = owner.Email,
                        Role = owner.Role.ConvertIntToString(),
                        Gym = owner.Gym is null ? null! : new GymDTO
                        {
                            Id = owner.Gym.Id,
                            Name = owner.Gym.Name,
                            Status = owner.Gym.Status,
                            CreatedAt = owner.Gym.CreatedAt,
                        },
                        Reviews = owner.OwnerReviews is null ? [] :
                            owner.OwnerReviews.Select(ownerReview => ownerReview == null ? null! : ownerReview.Review)
                            .Select(review => review == null ? null! : new ReviewDTO
                            {
                                Id = review.Id,
                                Title = review.Title,
                                Text = review.Text,
                                Status = review.Status,
                                CreatedAt = review.CreatedAt,
                            }).ToList(),
                    }).ToList(),
            Reviews = pokemon.Reviews is null ? [] :
                pokemon.Reviews.Select(review => new ReviewDTO
                {
                    Id = review.Id,
                    Title = review.Title,
                    Text = review.Text,
                    Status = review.Status,
                    CreatedAt = review.CreatedAt,
                    Pokemon = review.Pokemon is null ? null! : new PokemonDTO
                    {
                        Id = review.Pokemon.Id,
                        Name = review.Pokemon.Name,
                        BirthDate = review.Pokemon.BirthDate,
                        Status = review.Pokemon.Status,
                        Categories = review.Pokemon.PokemonCategories is null ? [] :
                            review.Pokemon.PokemonCategories
                            .Select(pc => pc.Category)
                            .Select(c => c == null ? null! : new CategoryDTO
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Status = c.Status,
                            }),
                    },
                }).ToList(),
        };
}
