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
                        }).ToList(),
              })
              .ToList(),
      };
}