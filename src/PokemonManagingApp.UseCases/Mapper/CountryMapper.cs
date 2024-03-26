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
            IsDeleted = country.IsDeleted,
            CreatedAt = country.CreatedAt,
            DeletedAt = country.DeletedAt,
            Owners = country.Owners is null ? [] :
                  country.Owners.Select(owner => owner == null ? null! : new OwnerDTO
                  {
                      Id = owner.Id,
                      UserName = owner.UserName ?? string.Empty,
                      IsDeleted = owner.IsDeleted,
                      CreatedAt = owner.CreatedAt,
                      Email = owner.Email,
                      DeletedAt = owner.DeletedAt,
                      Role = owner.Role.ConvertIntToString(),
                      Country = owner.Country is null ? null! : new CountryDTO
                      {
                          Id = owner.Country.Id,
                          Name = owner.Country.Name,
                          IsDeleted = owner.Country.IsDeleted,
                          CreatedAt = owner.Country.CreatedAt,
                          DeletedAt = owner.Country.DeletedAt,
                      },
                      Gym = owner.Gym is null ? null! : new GymDTO
                      {
                          Id = owner.Gym.Id,
                          Name = owner.Gym.Name,
                          IsDeleted = owner.Gym.IsDeleted,
                          CreatedAt = owner.Gym.CreatedAt,
                          DeletedAt = owner.Gym.DeletedAt,
                      },
                      Reviews = owner.Reviews is null ? [] :
                      owner.Reviews.Select(review => review == null ? null! : new ReviewDTO
                      {
                          Id = review.Id,
                          Title = review.Title,
                          Text = review.Text,
                          IsDeleted = review.IsDeleted,
                          CreatedAt = review.CreatedAt,
                          DeletedAt = review.DeletedAt,
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
                                Categories = pokemon.PokemonCategories is null ? [] :
                                  pokemon.PokemonCategories
                                  .Select(pc => pc.Category)
                                  .Select(category => category == null ? null! : new CategoryDTO
                                  {
                                      Id = category.Id,
                                      Name = category.Name,
                                      IsDeleted = category.IsDeleted,
                                      CreatedAt = category.CreatedAt,
                                      DeletedAt = category.DeletedAt,
                                  }).ToList(),
                                Reviews = pokemon.Reviews is null ? [] :
                                  pokemon.Reviews.Select(review => review == null ? null! : new ReviewDTO
                                  {
                                      Id = review.Id,
                                      Title = review.Title,
                                      Text = review.Text,
                                      IsDeleted = review.IsDeleted,
                                      CreatedAt = review.CreatedAt,
                                      DeletedAt = review.DeletedAt,
                                  }).ToList(),
                            }),
                  })
        };
}