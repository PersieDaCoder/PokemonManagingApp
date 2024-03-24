
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.Result;
using MediatR;
using PokemonManagingApp.Core.Interfaces.Data.Repositories;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.UseCase.DTOs;
using PokemonManagingApp.UseCases.Mapper;

namespace PokemonManagingApp.UseCases.UseCase_Owners.Commands.Login;

public class LoginHandler(IOwnerRepository ownerRepository) : IRequestHandler<LoginCommand, Result<OwnerDTO>>
{
  private readonly IOwnerRepository _ownerRepository = ownerRepository;

  public async Task<Result<OwnerDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    Owner? owner = await _ownerRepository.GetEntityByConditionAsync(owner => owner.Email == request.Email, false);
    if (owner is null) return Result<OwnerDTO>.NotFound("Email is not found");
    if (owner.Password != request.Password) return Result<OwnerDTO>.Error("Password is incorrect");

    return Result<OwnerDTO>.Success(owner.MapToDTO());

    // var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SVjmMJe8OQVInwKTrcOGZQxhblWYXHXm7xVLMzYnKXY"));
    // var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    // Claim[] claims = {
    //    new Claim(JwtRegisteredClaimNames.Sub, owner.UserName),
    //     new Claim(JwtRegisteredClaimNames.Email, owner.Email),
    //     new Claim(JwtRegisteredClaimNames.Sid, owner.Id.ToString())
    //     };

    // JwtSecurityToken Sectoken = new()
    // {
      
    // };
    // SecurityToken token = new JwtSecurityTokenHandler().CreateToken(Sectoken);
    // return Result<TokenDTO>.Success(new TokenDTO
    // {
    //   AccessToken = token
    // });
  }
}