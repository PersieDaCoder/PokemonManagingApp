using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Helpers;

namespace PokemonManagingApp.UseCases.Mapper;

public static class CountryMapper
{
    public static CountryDTO MapToDTO(this Country country)

        => new CountryDTO
        {
            Id = country.Id,
            Name = country.Name,
            Status = country.Status,
            CreatedAt = country.CreatedAt,
            Owners = country.Owners is null ? [] :
                  country.Owners.Select(owner => owner == null ? null! : new OwnerDTO
                  {
                      Id = owner.Id,
                      UserName = owner.UserName ?? string.Empty,
                      Status = owner.Status,
                      CreatedAt = owner.CreatedAt,
                      Email = owner.Email,
                      Role = owner.Role.ConvertIntToString(),
                      Country = owner.Country is null ? null! : new CountryDTO
                      {
                          Id = owner.Country.Id,
                          Name = owner.Country.Name,
                          Status = owner.Country.Status
                      },
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
                      Pokemons = owner.PokemonOwners is null ? [] :
                            owner.PokemonOwners
                            .Select(po => po.Pokemon)
                            .Select(pokemon => pokemon == null ? null! : new PokemonDTO
                            {
                                Id = pokemon.Id,
                                Name = pokemon.Name,
                                Status = pokemon.Status,
                                BirthDate = pokemon.BirthDate,
                                Categories = pokemon.PokemonCategories is null ? [] :
                                  pokemon.PokemonCategories
                                  .Select(pc => pc.Category)
                                  .Select(category => category == null ? null! : new CategoryDTO
                                  {
                                      Id = category.Id,
                                      Name = category.Name,
                                      Status = category.Status
                                  }).ToList(),
                                Reviews = pokemon.Reviews is null ? [] :
                                  pokemon.Reviews.Select(review => review == null ? null! : new ReviewDTO
                                  {
                                      Id = review.Id,
                                      Title = review.Title,
                                      Text = review.Text,
                                      Status = review.Status,
                                      CreatedAt = review.CreatedAt,
                                  }).ToList(),
                            }),
                  })
        };
}