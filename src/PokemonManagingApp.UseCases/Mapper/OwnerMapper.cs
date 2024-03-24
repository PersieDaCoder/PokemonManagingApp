using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Helpers;

namespace PokemonManagingApp.UseCases.Mapper;

public static class OwnerMapper
{
    public static OwnerDTO MapToDTO(this Owner owner)
      => new OwnerDTO
      {
          Id = owner.Id,
          UserName = owner.UserName ?? string.Empty,
          Status = owner.Status,
          Email = owner.Email,
          CreatedAt = owner.CreatedAt,
          Role = owner.Role.ConvertIntToString(),
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
                                    Reviews = p.Reviews is null ? [] :
                                        p.Reviews.Select(review => new ReviewDTO
                                        {
                                            Id = review.Id,
                                            Title = review.Title,
                                            Text = review.Text,
                                            Status = review.Status,
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