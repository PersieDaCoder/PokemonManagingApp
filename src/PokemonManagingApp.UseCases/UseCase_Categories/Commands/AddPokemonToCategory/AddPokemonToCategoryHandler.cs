using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.AddPokemonToCategory;

public class AddPokemonToCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddPokemonToCategoryCommand, Result<PokemonDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PokemonDTO>> Handle(AddPokemonToCategoryCommand request, CancellationToken cancellationToken)
    {
        // Check if the pokemon and category exist
        Pokemon? pokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id.Equals(request.PokemonId), false);
        if (pokemon is null) 
            return Result.NotFound("Pokemon is not found");
        // Check if the category exist
        Category? category = await _unitOfWork.CategoryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.CategoryId), false);
        if (category is null) 
            return Result.NotFound("Category is not found");
        // Check if the pokemon is already in the category
        if (pokemon.PokemonCategories.Any(pokemonCategory => pokemonCategory.CategoryId.Equals(request.CategoryId)
        && pokemonCategory.PokemonId.Equals(request.PokemonId))) 
            return Result.Error("Pokemon is already in the category");
        _unitOfWork.PokemonCategoryRepository.Add(new PokemonCategory
        {
            CategoryId = request.CategoryId,
            PokemonId = request.PokemonId,
        });
        // Save changes
        await _unitOfWork.SaveChangesAsync();
        return Result.Success(pokemon.MapToDTO(),"Pokemon is added to the category successfully");
    }
}