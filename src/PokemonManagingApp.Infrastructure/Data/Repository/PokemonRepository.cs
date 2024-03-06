using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.DTOs.Category;
using PokemonManagingApp.Core.DTOs.Country;
using PokemonManagingApp.Core.DTOs.Owner;
using PokemonManagingApp.Core.DTOs.Pokemon;
using PokemonManagingApp.Core.DTOs.Review;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PokemonManagingApp.Infrastructure.Data.Repository;

public class PokemonRepository : BaseRepository<Pokemon>, IPokemonRepository
{
  public PokemonRepository(ApplicationDBContext context) : base(context)
  {
  }

  public async Task<IEnumerable<PokemonDTO>> GetAllDTOs()
    => await _dbSet
    .Select(pokemon => new PokemonDTO
    {
      Id = pokemon.Id,
      Name = pokemon.Name,
      BirthDate = pokemon.BirthDate,

      Categories = pokemon.PokemonCategories
      .Where(pokemonCategory => pokemonCategory.PokemonId == pokemon.Id)
        .Select(pokemonCategory => pokemonCategory.Category)
        .Select(category => new CategoryDTO
        {
          Id = category != null ? category.Id : default,
          Name = category != null ? category.Name : string.Empty
        }).ToList(),

      Owners = pokemon.PokemonOwners
      .Where(pokemonOwner => pokemonOwner.PokemonId == pokemon.Id)
        .Select(pokemonOwner => pokemonOwner.Owner)
        .Select(owner => new OwnerDTO
        {
          Id = owner != null ? owner.Id : default,
          Gym = owner != null ? owner.Gym : string.Empty,
          Name = owner != null ? owner.Name : string.Empty,
          CountryId = owner != null ? owner.CountryId : default,
          Country = owner != null ? new CountryDTO
          {
            Id = owner.Country != null ? owner.Country.Id : default,
            Name = owner.Country != null ? owner.Country.Name : string.Empty
          } : null
        }).ToList(),

        Reviews = pokemon.Reviews.Select(review => new ReviewDTO
        {
          Id = review.Id,
          Title = review.Title,
          PokemonId = pokemon.Id,
          ReviewerId = review.ReviewerId,
          Text = review.Text,
        }).ToList()
    }).ToListAsync();
}