using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result>
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
  {
    // Check if the category is in the database
    Category? selectedCategory =
      await _unitOfWork.CategoryRepository
        .GetEntityByConditionAsync(c => c.Id.Equals(request.Id), true);
    if (selectedCategory is null)
      return Result.NotFound();
    //Update the category
    selectedCategory.Name = request.Name;
    await _unitOfWork.SaveChangesAsync();
    return Result.SuccessWithMessage("Category is updated successfully");
  }
}