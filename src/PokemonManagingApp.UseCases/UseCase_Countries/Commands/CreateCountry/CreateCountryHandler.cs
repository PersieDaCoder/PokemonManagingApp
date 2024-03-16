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

namespace PokemonManagingApp.UseCases.UseCase_Countries.Commands.CreateCountry;

public class CreateCountryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCountryCommand, Result<CountryDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CountryDTO>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        Country? checkedCountry = await _unitOfWork.CountryRepository
            .DBSet()
            .FirstOrDefaultAsync(c => c.Name.Equals(request.Name));
        if(checkedCountry is not null) return Result<CountryDTO>.Error("Country already exists");
        Country addingCountry = new Country
        {
            Name = request.Name,
        };
        _unitOfWork.CountryRepository.Add(addingCountry);
        await _unitOfWork.SaveChangesAsync();
        return Result<CountryDTO>.Success(CountryMapper.MapToDTO(addingCountry));
    }
}