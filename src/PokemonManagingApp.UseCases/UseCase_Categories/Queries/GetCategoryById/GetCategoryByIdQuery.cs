using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery : IRequest<Result<CategoryDTO>>
{
    public Guid Id { get; init; }
}