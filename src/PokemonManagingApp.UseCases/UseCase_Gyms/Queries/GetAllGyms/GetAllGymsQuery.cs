using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetAllGyms;

public record GetAllGymsQuery : IRequest<Result<IEnumerable<GymDTO>>>
{}