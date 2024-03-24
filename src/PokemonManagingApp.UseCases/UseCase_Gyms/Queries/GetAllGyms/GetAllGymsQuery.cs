using Ardalis.Result;
using MediatR;
using PokemonManagingApp.UseCases.DTOs;

namespace PokemonManagingApp.UseCases.UseCase_Gyms.Queries.GetAllGyms;

public record GetAllGymsQuery : IRequest<Result<IEnumerable<GymDTO>>>
{}