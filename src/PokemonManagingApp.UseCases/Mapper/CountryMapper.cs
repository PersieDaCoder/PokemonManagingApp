using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.Mapper;

public static class CountryMapper
{
    public static CountryDTO MapToDTO(this Country country)

        => new CountryDTO
        {
            Id = country.Id,
            Name = country.Name,
            Status = country.Status,
            Owners = country.Owners is null ? new List<OwnerDTO>() :
                  country.Owners.Select(owner => owner == null ? null! : new OwnerDTO
                  {
                      Id = owner.Id,
                      Name = owner.Name,
                      Status = owner.Status,
                      CountryId = owner.CountryId,
                      Gym = owner.Gym,
                      Pokemon = owner.PokemonOwners is null ? new List<PokemonDTO>() :
                          owner.PokemonOwners
                          .Select(po => po.Pokemon)
                          .Select(pokemon => pokemon == null ? null! : new PokemonDTO
                          {
                              Id = pokemon.Id,
                              Name = pokemon.Name,
                              Status = pokemon.Status,
                              BirthDate = pokemon.BirthDate,
                              Categories = pokemon.PokemonCategories is null ? new List<CategoryDTO>() :
                                  pokemon.PokemonCategories
                                  .Select(pc => pc.Category)
                                  .Select(category => category == null ? null! : new CategoryDTO
                                  {
                                      Id = category.Id,
                                      Name = category.Name,
                                      Status = category.Status
                                  }).ToList(),
                              Owners = pokemon.PokemonOwners is null ? new List<OwnerDTO>() :
                                  pokemon.PokemonOwners
                                  .Select(po => po.Owner)
                                  .Select(owner => owner == null ? null! : new OwnerDTO
                                  {
                                      Id = owner.Id,
                                      Name = owner.Name,
                                      Status = owner.Status,
                                      CountryId = owner.CountryId,
                                      Gym = owner.Gym,
                                  }).ToList(),
                              Reviews = pokemon.Reviews is null ? new List<ReviewDTO>() :
                                    pokemon.Reviews.Select(review => review == null ? null! : new ReviewDTO
                                    {
                                        Id = review.Id,
                                        ReviewerId = review.ReviewerId,
                                        Text = review.Text,
                                        Title = review.Title,
                                        PokemonId = review.PokemonId,
                                        Status = review.Status
                                    }).ToList()
                          }).ToList()
                  }),
        };
}