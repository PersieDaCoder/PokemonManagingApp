using PokemonManagingApp.Core.Interfaces.Data;
using MediatR;
using PokemonManagingApp.Core.DTOs.Category;
using PokemonManagingApp.Core.DTOs.Pokemon;
using PokemonManagingApp.Core.DTOs.Owner;
using PokemonManagingApp.Core.DTOs.Country;
using PokemonManagingApp.Core.DTOs.Review;

namespace PokemonManagingApp.UseCases.Pokemon.Queries.GetAllPokemons;

public class GetAllPokemonsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPokemonsQuery, IEnumerable<PokemonDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<PokemonDTO>> Handle(GetAllPokemonsQuery request, CancellationToken cancellationToken)
     => (await _unitOfWork.PokemonRepository.GetEntitiesByConditionAsync(p => p.Status,true)).Select(pokemon => new PokemonDTO
     {
         Id = pokemon.Id,
         Name = pokemon.Name,
         BirthDate = pokemon.BirthDate,
         Status = pokemon.Status,

         Categories = pokemon.PokemonCategories is not null ? pokemon.PokemonCategories.Select(pc => new CategoryDTO
         {
             Id = pc.Category is not null ? pc.Category.Id : default,
             Name = pc.Category is not null ? pc.Category.Name : string.Empty,
             Status = pc.Category is not null ? pc.Category.Status : default
         }) : new List<CategoryDTO>(),

         Owners = pokemon.PokemonOwners
    .Where(pokemonOwner => pokemonOwner.PokemonId == pokemon.Id)
      .Select(pokemonOwner => pokemonOwner.Owner)
      .Select(owner => new OwnerDTO
      {
          Id = owner?.Id ?? default,
          Gym = owner?.Gym ?? string.Empty,
          Name = owner?.Name ?? string.Empty,
          CountryId = owner?.CountryId ?? default,
          Status = owner?.Status ?? default,
          Country = owner?.Country is not null ? new CountryDTO
          {
              Id = owner.Country is not null ? owner.Country.Id : default,
              Name = owner.Country is not null ? owner.Country.Name : string.Empty,
              Status = owner.Country is not null ? owner.Country.Status : default
          } : null
      }).ToList(),
         Reviews = pokemon.Reviews.Select(review => new ReviewDTO
         {
             Id = review.Id,
             Title = review.Title,
             PokemonId = pokemon.Id,
             ReviewerId = review.ReviewerId,
             Text = review.Text,
             Status = review.Status,
         }).ToList()
     });
}