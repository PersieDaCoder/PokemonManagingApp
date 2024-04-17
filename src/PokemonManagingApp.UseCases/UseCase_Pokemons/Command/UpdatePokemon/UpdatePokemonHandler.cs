using PokemonManagingApp.Core.Interfaces.Data;
using MediatR;
using Ardalis.Result;

namespace PokemonManagingApp.Pokemon_UseCase;

public class UpdatePokemonHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePokemonCommand, Result>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result> Handle(UpdatePokemonCommand request, CancellationToken cancellationToken)
  {
    // Check if the Pokemon exists
    Core.Models.Pokemon? pokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id == request.Id, true);
    if (pokemon is null) return Result.NotFound();
    // Check if the Pokemon Category exists
    Core.Models.PokemonCategory? pokemonCategory = await _unitOfWork.PokemonCategoryRepository.GetEntityByConditionAsync(p => p.Id == request.CategoryId, true);
    if (pokemonCategory is null) return Result.NotFound();
    // Update the Pokemon
    {
      pokemon.Name = request.Name;
      pokemon.BirthDate = request.BirthDate;
      pokemon.Description = request.Description;
      pokemon.ImageUrl = request.ImageUrl;
      pokemon.Height = request.Height;
      pokemon.Weight = request.Weight;
    }
    // Update the Pokemon Category
    {
      pokemonCategory.CategoryId = request.CategoryId;
    }
    await _unitOfWork.SaveChangesAsync();
    return Result.Success();
  }

}