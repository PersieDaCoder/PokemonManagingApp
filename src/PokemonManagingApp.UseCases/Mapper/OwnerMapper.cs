using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.Helpers;

namespace PokemonManagingApp.UseCases.Mapper;

public static class OwnerMapper
{
    public static OwnerDTO MapToDTO(this Owner owner)
      => new OwnerDTO
      {
          Id = owner.Id,
          UserName = owner.UserName ?? string.Empty,
          IsDeleted = owner.IsDeleted,
          Email = owner.Email,
          CreatedAt = owner.CreatedAt,
          DeletedAt = owner.DeletedAt,
          ImageUrl = owner.ImageUrl,
          Role = owner.Role.ConvertIntToString(),
          Gym = owner.Gym is null ? null! : new GymDTO
          {
              Id = owner.Gym.Id,
              Name = owner.Gym.Name,
              IsDeleted = owner.Gym.IsDeleted,
              CreatedAt = owner.Gym.CreatedAt,
              DeletedAt = owner.Gym.DeletedAt,
          },
          Reviews = owner.Reviews is null ? [] : owner.Reviews.Select(review => new ReviewDTO
          {
              Id = review.Id,
              Title = review.Title,
              Text = review.Text,
              IsDeleted = review.IsDeleted,
              CreatedAt = review.CreatedAt,
              DeletedAt = review.DeletedAt,
              Pokemon = review.Pokemon is null ? null! : new PokemonDTO
              {
                  Id = review.Pokemon.Id,
                  Name = review.Pokemon.Name,
                  BirthDate = review.Pokemon.BirthDate,
                  IsDeleted = review.Pokemon.IsDeleted,
                  CreatedAt = review.Pokemon.CreatedAt,
                  DeletedAt = review.Pokemon.DeletedAt,
                  Description = review.Pokemon.Description,
                  ImageUrl = review.Pokemon.ImageUrl,
                  Height = review.Pokemon.Height,
                  Weight = review.Pokemon.Weight,
                  Categories = review.Pokemon.PokemonCategories is null ? [] :
                      review.Pokemon.PokemonCategories
                      .Select(pc => pc.Category)
                      .Select(c => c == null ? null! : new CategoryDTO
                      {
                          Id = c.Id,
                          Name = c.Name,
                          IsDeleted = c.IsDeleted,
                          CreatedAt = c.CreatedAt,
                          DeletedAt = c.DeletedAt,
                          Pokemons = c.PokemonCategories is null ? [] :
                              c.PokemonCategories
                              .Select(pc => pc.Pokemon)
                              .Select(p => p == null ? null! : new PokemonDTO
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  BirthDate = p.BirthDate,
                                  IsDeleted = p.IsDeleted,
                                  CreatedAt = p.CreatedAt,
                                  DeletedAt = p.DeletedAt,
                                  Description = p.Description,
                                  ImageUrl = p.ImageUrl,
                                  Height = p.Height,
                                  Weight = p.Weight,
                                  Reviews = p.Reviews is null ? [] :
                                      p.Reviews.Select(review => new ReviewDTO
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
              }
          }).ToList(),
          Country = owner.Country is null ? null! : new CountryDTO
          {
              Id = owner.Country.Id,
              Name = owner.Country.Name,
              IsDeleted = owner.Country.IsDeleted,
              CreatedAt = owner.Country.CreatedAt,
              DeletedAt = owner.Country.DeletedAt,
          },
          Pokemons = owner.PokemonOwners is null ? [] :
              owner.PokemonOwners
              .Select(po => po.Pokemon)
              .Select(p => p == null ? null! : new PokemonDTO
              {
                  Id = p.Id,
                  Name = p.Name,
                  BirthDate = p.BirthDate,
                  IsDeleted = p.IsDeleted,
                  CreatedAt = p.CreatedAt,
                  DeletedAt = p.DeletedAt,
                  Description = p.Description,
                  ImageUrl = p.ImageUrl,
                  Height = p.Height,
                  Weight = p.Weight,
                  Categories = p.PokemonCategories is null ? [] :
                        p.PokemonCategories
                        .Select(pc => pc.Category)
                        .Select(c => c == null ? null! : new CategoryDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                            IsDeleted = c.IsDeleted,
                            CreatedAt = c.CreatedAt,
                            DeletedAt = c.DeletedAt,
                            Pokemons = c.PokemonCategories is null ? [] :
                                c.PokemonCategories
                                .Select(pc => pc.Pokemon)
                                .Select(p => p == null ? null! : new PokemonDTO
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    BirthDate = p.BirthDate,
                                    IsDeleted = p.IsDeleted,
                                    CreatedAt = p.CreatedAt,
                                    DeletedAt = p.DeletedAt,
                                    Description = p.Description,
                                    ImageUrl = p.ImageUrl,
                                    Height = p.Height,
                                    Weight = p.Weight,
                                    Reviews = p.Reviews is null ? [] :
                                        p.Reviews.Select(review => new ReviewDTO
                                        {
                                            Id = review.Id,
                                            Title = review.Title,
                                            Text = review.Text,
                                            IsDeleted = review.IsDeleted,
                                            CreatedAt = review.CreatedAt,
                                            DeletedAt = review.DeletedAt,
                                        }).ToList(),
                                }),
                        }),
                  Reviews = p.Reviews is null ? [] :
                        p.Reviews.Select(review => new ReviewDTO
                        {
                            Id = review.Id,
                            Title = review.Title,
                            Text = review.Text,
                            IsDeleted = review.IsDeleted,
                            CreatedAt = review.CreatedAt,
                            DeletedAt = review.DeletedAt,
                            Pokemon = review.Pokemon is null ? null! : new PokemonDTO
                            {
                                Id = review.Pokemon.Id,
                                Name = review.Pokemon.Name,
                                BirthDate = review.Pokemon.BirthDate,
                                IsDeleted = review.Pokemon.IsDeleted,
                                CreatedAt = review.Pokemon.CreatedAt,
                                DeletedAt = review.Pokemon.DeletedAt,
                                Description = review.Pokemon.Description,
                                ImageUrl = review.Pokemon.ImageUrl,
                                Height = review.Pokemon.Height,
                                Weight = review.Pokemon.Weight,
                                Categories = review.Pokemon.PokemonCategories is null ? [] :
                                    review.Pokemon.PokemonCategories
                                    .Select(pc => pc.Category)
                                    .Select(c => c == null ? null! : new CategoryDTO
                                    {
                                        Id = c.Id,
                                        Name = c.Name,
                                        IsDeleted = c.IsDeleted,
                                        CreatedAt = c.CreatedAt,
                                        DeletedAt = c.DeletedAt,
                                        Pokemons = c.PokemonCategories is null ? [] :
                                            c.PokemonCategories
                                            .Select(pc => pc.Pokemon)
                                            .Select(p => p == null ? null! : new PokemonDTO
                                            {
                                                Id = p.Id,
                                                Name = p.Name,
                                                BirthDate = p.BirthDate,
                                                IsDeleted = p.IsDeleted,
                                                CreatedAt = p.CreatedAt,
                                                DeletedAt = p.DeletedAt,
                                                Description = p.Description,
                                                ImageUrl = p.ImageUrl,
                                                Height = p.Height,
                                                Weight = p.Weight,
                                                Reviews = p.Reviews is null ? [] :
                                                    p.Reviews.Select(review => new ReviewDTO
                                                    {
                                                        Id = review.Id,
                                                        Title = review.Title,
                                                        Text = review.Text,
                                                        IsDeleted = review.IsDeleted,
                                                        CreatedAt = review.CreatedAt,
                                                        DeletedAt = review.DeletedAt,
                                                    }).ToList(),
                                            }),
                                    }),
                            },
                        }).ToList(),
              })
              .ToList(),
      };
}