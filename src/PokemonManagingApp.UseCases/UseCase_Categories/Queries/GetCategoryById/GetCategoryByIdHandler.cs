using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Queries.GetCategoryById;

public class GetAllCategoryByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<CategoryDTO>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        CategoryDTO? category = await _unitOfWork.CategoryRepository
                                                .DBSet()
                                                .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
                                                .Where(c => c.Id.Equals(request.Id))
                                                .Select(c => CategoryMapper.MapToDTO(c))
                                                .SingleOrDefaultAsync();
        if (category is null) return Result<CategoryDTO>.NotFound();
        return Result<CategoryDTO>.Success(category);
    }
}