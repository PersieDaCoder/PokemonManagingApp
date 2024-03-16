using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.Mapper;

public static class CategoryMapper
{
    public static CategoryDTO MapToDTO(this Category category)
    {
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Status = category.Status,
            Pokemons = category.PokemonCategories is null ? new List<PokemonDTO>() :
                category.PokemonCategories
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
                        Reviews = p.Reviews is not null ?
                            p.Reviews.Select(review => new ReviewDTO
                            {
                                Id = review.Id,
                                Title = review.Title,
                                PokemonId = p.Id,
                                ReviewerId = review.ReviewerId,
                                Text = review.Text,
                                Status = review.Status,
                            }).ToList() : [],
                            }).ToList(),
        };
    }
}
