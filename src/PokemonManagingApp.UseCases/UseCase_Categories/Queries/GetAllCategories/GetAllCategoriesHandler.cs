﻿using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCases.DTOs;
using PokemonManagingApp.UseCases.Mapper;
using PokemonManagingApp.UseCases.UseCase_Categories;

namespace PokemonManagingApp.UseCase.UseCase_Categories;

public class GetAllCategoriesHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryDTO>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<CategoryDTO>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync(cancellationToken);
        if (!categories.Any()) return Result<IEnumerable<CategoryDTO>>.NotFound();
        return Result<IEnumerable<CategoryDTO>>.Success(categories.Select(c => CategoryMapper.MapToDTO(c)));
    }
}
