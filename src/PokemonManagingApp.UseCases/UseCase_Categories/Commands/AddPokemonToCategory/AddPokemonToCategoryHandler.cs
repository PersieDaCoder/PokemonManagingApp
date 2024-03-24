using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.AddPokemonToCategory;

public class AddPokemonToCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddPokemonToCategoryCommand, Result<PokemonDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PokemonDTO>> Handle(AddPokemonToCategoryCommand request, CancellationToken cancellationToken)
    {
        Pokemon? pokemon = await _unitOfWork.PokemonRepository.GetEntityByConditionAsync(p => p.Id.Equals(request.PokemonId), false);
        if (pokemon is null) return Result<PokemonDTO>.NotFound("Pokemon is not found");
        Category? category = await _unitOfWork.CategoryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.CategoryId), false);
        if (category is null) return Result<PokemonDTO>.NotFound("Category is not found");
        if (pokemon.PokemonCategories.Any(p => p.CategoryId.Equals(request.CategoryId)
        && p.PokemonId.Equals(request.PokemonId))) return Result<PokemonDTO>.Error("Pokemon is already in the category");
        pokemon.PokemonCategories.Add(new PokemonCategory
        {
            CategoryId = request.CategoryId,
            PokemonId = request.PokemonId,
        });
        await _unitOfWork.SaveChangesAsync();
        return Result<PokemonDTO>.Success(pokemon.MapToDTO());
    }
}