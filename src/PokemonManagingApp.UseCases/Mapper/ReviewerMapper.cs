using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;

namespace PokemonManagingApp.UseCases.Mapper;

public static class ReviewerMapper
{
    public static ReviewerDTO MapToDTO(this Reviewer reviewer)
    => new ReviewerDTO
    {
        Id = reviewer.Id,
        FirstName = reviewer.FirstName,
        LastName = reviewer.LastName,
        Status = reviewer.Status,
        Reviews = reviewer.Reviews is null ? [] :
            reviewer.Reviews.Select(r => ReviewMapper.MapToDTO(r))
            .ToList(),
    };

}