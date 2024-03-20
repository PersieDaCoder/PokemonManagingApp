using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.RemovePokemonOutOfCategory;

public class RemovePokemonOutOfCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemovePokemonOutOfCategoryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RemovePokemonOutOfCategoryCommand request, CancellationToken cancellationToken)
    {
        // check if the pokemon is in the database
        Pokemon? pokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id.Equals(request.PokemonId), false);
        if (pokemon is null) return Result.NotFound("Pokemon is not found");
        // check if the category is in the database
        Category? category = await _unitOfWork.CategoryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.CategoryId), false);
        if (category is null) return Result.NotFound("Category is not found");
        // check if the pokemon is in the category
        PokemonCategory? pokemonCategory = await _unitOfWork.PokemonCategoryRepository.GetEntityByConditionAsync(pc => pc.PokemonId.Equals(request.PokemonId) && pc.CategoryId.Equals(request.CategoryId), false);
        if (pokemonCategory is null) return Result.NotFound("Pokemon is not in the category");
        _unitOfWork.PokemonCategoryRepository.Remove(pokemonCategory);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}