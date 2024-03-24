using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.Mapper;

public static class ReviewMapper
{
    public static ReviewDTO MapToDTO(Review review)
    {
        return new ReviewDTO
        {
            Id = review.Id,
            Text = review.Text,
            Status = review.Status,
            Title = review.Title,
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
                Owners = review.Pokemon.PokemonOwners is null ? [] :
                    review.Pokemon.PokemonOwners
                        .Where(pokemonOwner => pokemonOwner.PokemonId == review.Pokemon.Id)
                        .Select(pokemonOwner => pokemonOwner.Owner)
                        .Select(owner => owner == null ? null! : new OwnerDTO
                        {
                            Id = owner.Id,
                            Gym = owner.Gym,
                            UserName = owner.UserName ?? string.Empty,
                            CountryId = owner.CountryId,
                            Status = owner.Status,
                            Country = owner.Country is null ? null! : new CountryDTO
                            {
                                Id = owner.Country.Id,
                                Name = owner.Country.Name,
                                Status = owner.Country.Status,
                            }
                        }).ToList(),
            },
            Reviewer = review.Reviewer is null ? null! : new ReviewerDTO
            {
                Id = review.Reviewer.Id,
                FullName = $"{review.Reviewer.FirstName} {review.Reviewer.LastName}",
                Status = review.Reviewer.Status,
            }
        };
    }
}