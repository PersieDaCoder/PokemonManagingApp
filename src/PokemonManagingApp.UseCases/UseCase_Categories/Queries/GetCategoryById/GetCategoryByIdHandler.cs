using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Queries.GetCategoryById;

public class GetAllCategoryByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<CategoryDTO>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Category? category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.Id, false);
        if (category is null) return Result<CategoryDTO>.NotFound();
        return Result<CategoryDTO>.Success(CategoryMapper.MapToDTO(category));
    }
}