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
            Owners = country.Owners is null ? [] :
                  country.Owners.Select(owner => owner == null ? null! : new OwnerDTO
                  {
                      Id = owner.Id,
                      Name = owner.Name,
                      Status = owner.Status,
                      CountryId = owner.CountryId,
                      Gym = owner.Gym,
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
                                Owners = pokemon.PokemonOwners is null ? [] :
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
                                Reviews = pokemon.Reviews is null ? [] :
                                    pokemon.Reviews.Select(review => review == null ? null! : new ReviewDTO
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
                                    }).ToList()
                            }).ToList()
                  }),
        };
}