using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Countries.Queries.GetCountryById;

public record GetCountryByIdQuery : IRequest<Result<CountryDTO>>
{
    public Guid Id { get; init; }
}