using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.DisableCategory;

public class DisableCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DisableCategoryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DisableCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? checkedCategory = await _unitOfWork.CategoryRepository.GetEntityByConditionAsync(c => c.Id.Equals(request.Id), true);
        if (checkedCategory is null) 
            return Result.NotFound("Category is not found");
        checkedCategory.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.SuccessWithMessage("Category is disabled successfully");
    }
}