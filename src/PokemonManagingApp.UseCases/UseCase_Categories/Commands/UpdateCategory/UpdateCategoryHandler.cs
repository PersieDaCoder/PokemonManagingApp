using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
  {
    Category? selectedCategory = await _unitOfWork.CategoryRepository
            .DBSet()
            .Include(c => c.PokemonCategories).ThenInclude(pc => pc.Pokemon)
            .SingleOrDefaultAsync(c => c.Id.Equals(request.Id));
        if (selectedCategory is null) return Result.NotFound();
        selectedCategory.Name = request.Name;
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
  }
}