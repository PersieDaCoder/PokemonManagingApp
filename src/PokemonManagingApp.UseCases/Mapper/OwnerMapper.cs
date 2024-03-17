using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.Mapper;

public static class OwnerMapper
{
    public static OwnerDTO MapToDTO(this Owner owner)
      => new OwnerDTO
      {
          Id = owner.Id,
          Name = owner.Name,
          CountryId = owner.CountryId,
          Gym = owner.Gym,
          Status = owner.Status,
          Country = owner.Country is null ? null! : new CountryDTO
          {
              Id = owner.Country.Id,
              Name = owner.Country.Name,
              Status = owner.Country.Status
          },
          Pokemons = owner.PokemonOwners is null ? [] :
              owner.PokemonOwners
              .Select(po => po.Pokemon)
              .Select(p => p == null ? null! : new PokemonDTO
              {
                  Id = p.Id,
                  Name = p.Name,
                  BirthDate = p.BirthDate,
                  Status = p.Status,
                  Categories = p.PokemonCategories is null ? [] :
                        p.PokemonCategories
                        .Select(pc => pc.Category)
                        .Select(c => c == null ? null! : new CategoryDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Status = c.Status,
                            Pokemons = c.PokemonCategories is null ? [] :
                                c.PokemonCategories
                                .Select(pc => pc.Pokemon)
                                .Select(p => p == null ? null! : new PokemonDTO
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    BirthDate = p.BirthDate,
                                    Status = p.Status,
                                    Owners = p.PokemonOwners is null ? [] :
                                    p.PokemonOwners
                                        .Where(pokemonOwner => pokemonOwner.PokemonId == p.Id)
                                        .Select(pokemonOwner => pokemonOwner.Owner)
                                        .Select(owner => owner == null ? null! : new OwnerDTO
                                        {
                                            Id = owner.Id,
                                            Name = owner.Name,
                                            CountryId = owner.CountryId,
                                            Gym = owner.Gym,
                                            Status = owner.Status,
                                            Country = owner.Country is null ? null! : new CountryDTO
                                            {
                                                Id = owner.Country.Id,
                                                Name = owner.Country.Name,
                                                Status = owner.Country.Status
                                            }
                                        }).ToList(),
                                    Reviews = p.Reviews is null ? [] :
                                        p.Reviews.Select(review => new ReviewDTO
                                        {
                                            Id = review.Id,
                                            Title = review.Title,
                                            Text = review.Text,
                                            Status = review.Status,
                                            Reviewer = review.Reviewer is null ? null! : new ReviewerDTO
                                            {
                                                Id = review.Reviewer.Id,
                                                FullName = $"{review.Reviewer.FirstName} {review.Reviewer.LastName}",
                                                Status = review.Reviewer.Status,
                                            },
                                        }).ToList(),
                                }),
                        }),
                  Reviews = p.Reviews is null ? [] :
                        p.Reviews.Select(review => new ReviewDTO
                        {
                            Id = review.Id,
                            Title = review.Title,
                            Text = review.Text,
                            Status = review.Status,
                            Reviewer = review.Reviewer is null ? null! : new ReviewerDTO
                            {
                                Id = review.Reviewer.Id,
                                FullName = $"{review.Reviewer.FirstName} {review.Reviewer.LastName}",
                                Status = review.Reviewer.Status,
                            },
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
              })
              .ToList(),
      };
}