using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<Result<CategoryDTO>>
{
    public string Name { get; init; } = null!;
}