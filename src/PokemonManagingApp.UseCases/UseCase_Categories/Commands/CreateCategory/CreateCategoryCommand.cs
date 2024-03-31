using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<Result<CategoryDTO>>
{
    public string Name { get; init; } = null!;
}