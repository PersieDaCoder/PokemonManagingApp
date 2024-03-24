using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Helpers;

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
            CreatedAt = category.CreatedAt,
            Pokemons = category.PokemonCategories is null ? [] :
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
                                UserName = owner.UserName ?? string.Empty,
                                Status = owner.Status,
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
                                Country = owner.Country is null ? null! : new CountryDTO
                                {
                                    Id = owner.Country.Id,
                                    Name = owner.Country.Name,
                                    Status = owner.Country.Status,
                                    CreatedAt = owner.Country.CreatedAt,
                                    Owners = owner.Country.Owners is null ? [] : owner.Country.Owners.Select(owner => owner == null ? null! : new OwnerDTO
                                    {
                                        Id = owner.Id,
                                        UserName = owner.UserName,
                                        Status = owner.Status,
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
                                        Reviews = owner.OwnerReviews is null ? [] : owner.OwnerReviews.Select(ownerReview => ownerReview == null ? null! : ownerReview.Review)
                                        .Select(review => review == null ? null! : new ReviewDTO
                                        {
                                            Id = review.Id,
                                            Title = review.Title,
                                            Text = review.Text,
                                            Status = review.Status,
                                            CreatedAt = review.CreatedAt,
                                        }).ToList(),
                                    })
                                }
                            }).ToList(),
                        Reviews = p.Reviews is null ? [] :
                            p.Reviews.Select(review => new ReviewDTO
                            {
                                Id = review.Id,
                                Title = review.Title,
                                Text = review.Text,
                                Status = review.Status,
                                CreatedAt = review.CreatedAt,
                            }).ToList(),
                    }).ToList(),
        };
    }
}
