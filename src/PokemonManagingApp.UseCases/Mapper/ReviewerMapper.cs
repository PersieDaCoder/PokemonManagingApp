using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.Mapper;

public static class ReviewerMapper
{
    public static ReviewerDTO MapToDTO(this Reviewer reviewer)
    => new ReviewerDTO
    {
        Id = reviewer.Id,
        FullName = $"{reviewer.FirstName} {reviewer.LastName}",
        Status = reviewer.Status,
        // Reviews = reviewer.Reviews is null ? [] :
        //     reviewer.Reviews.Select(r => r.Reviewer == null ? null! : new ReviewDTO
        //     {
        //         Id = r.Id,
        //         Title = r.Title,
        //         Text = r.Text,
        //         Status = r.Status,
        //         Pokemon = r.Pokemon is null ? null! : new PokemonDTO
        //         {
        //             Id = r.Pokemon.Id,
        //             Name = r.Pokemon.Name,
        //             BirthDate = r.Pokemon.BirthDate,
        //             Status = r.Pokemon.Status,
        //             Categories = r.Pokemon.PokemonCategories is null ? [] :
        //                 r.Pokemon.PokemonCategories
        //                 .Select(pc => pc.Category)
        //                 .Select(c => c == null ? null! : new CategoryDTO
        //                 {
        //                     Id = c.Id,
        //                     Name = c.Name,
        //                     Status = c.Status,
        //                 }),
        //             Owners = r.Pokemon.PokemonOwners is null ? [] :
        //                 r.Pokemon.PokemonOwners
        //                 .Where(po => po.PokemonId == r.Pokemon.Id)
        //                 .Select(po => po.Owner)
        //                 .Select(o => o == null ? null! : new OwnerDTO
        //                 {
        //                     Id = o.Id,
        //                     UserName = o.UserName ?? string.Empty,
        //                     CountryId = o.CountryId,
        //                     Gym = o.Gym,
        //                     Status = o.Status,
        //                     Country = o.Country is null ? null! : new CountryDTO
        //                     {
        //                         Id = o.Country.Id,
        //                         Name = o.Country.Name,
        //                         Status = o.Country.Status
        //                     },
        //                     Pokemons = o.PokemonOwners is null ? [] :
        //                         o.PokemonOwners
        //                         .Select(po => po.Pokemon)
        //                         .Select(p => p == null ? null! : new PokemonDTO
        //                         {
        //                             Id = p.Id,
        //                             Name = p.Name,
        //                             BirthDate = p.BirthDate,
        //                             Status = p.Status,
        //                             Categories = p.PokemonCategories is null ? [] :
        //                                 p.PokemonCategories
        //                                 .Select(pc => pc.Category)
        //                                 .Select(c => c == null ? null! : new CategoryDTO
        //                                 {
        //                                     Id = c.Id,
        //                                     Name = c.Name,
        //                                     Status = c.Status,
        //                                 }),
        //                         })
        //                         .ToList(),
        //                 })
        //     .ToList(),
        //         }
        //     }
    };
}