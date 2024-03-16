
using System.Runtime.Intrinsics.X86;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases;

public static class PokemonMapper
{
    public static PokemonDTO MapToPokemonDTO(this Pokemon pokemon)
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
                        Gym = owner.Gym,
                        Name = owner.Name,
                        CountryId = owner.CountryId,
                        Status = owner.Status,
                        Country = owner.Country is null ? null! : new CountryDTO
                        {
                            Id = owner.Country is not null ? owner.Country.Id : default,
                            Name = owner.Country is not null ? owner.Country.Name : string.Empty,
                            Status = owner.Country is not null ? owner.Country.Status : default
                        }
                    }).ToList(),
            Reviews = pokemon.Reviews is not null ?
                pokemon.Reviews.Select(review => new ReviewDTO
                {
                    Id = review.Id,
                    Title = review.Title,
                    PokemonId = pokemon.Id,
                    ReviewerId = review.ReviewerId,
                    Text = review.Text,
                    Status = review.Status,
                }).ToList() : [],
        };
}
