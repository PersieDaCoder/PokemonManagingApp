﻿using Ardalis.Result;
using MediatR;

namespace PokemonManagingApp.UseCases;

public record UpdatePokemonCommand : IRequest<Result>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTime BirthDate { get; init; }
}
