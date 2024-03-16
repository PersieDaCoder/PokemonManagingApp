using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Core.Interfaces.Data;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.DisableCategory;

public class DisableCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DisableCategoryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DisableCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? checkedCategory = await _unitOfWork.CategoryRepository
                .DBSet()
                .SingleOrDefaultAsync(category => category.Id == request.Id);
        if(checkedCategory is null) return Result.NotFound();
        checkedCategory.Status = false;
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}