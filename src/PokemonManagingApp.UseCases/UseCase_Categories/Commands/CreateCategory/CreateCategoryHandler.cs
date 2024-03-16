using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.CreateCategory;

public class CreateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<CategoryDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CategoryDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Category addingCategory = new() { Name = request.Name };
            _unitOfWork.CategoryRepository.Add(addingCategory);
            await _unitOfWork.SaveChangesAsync();
            return Result<CategoryDTO>.Success(CategoryMapper.MapToDTO(addingCategory));
        }
        catch (Exception e)
        {
            return Result<CategoryDTO>.Error(e.Message);
        }
    }
}