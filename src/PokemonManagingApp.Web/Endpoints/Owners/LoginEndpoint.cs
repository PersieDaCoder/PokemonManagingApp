using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonManagingApp.Core.DTOs;
using PokemonManagingApp.UseCases.UseCase_Owners.Commands.Login;
using Swashbuckle.AspNetCore.Annotations;

namespace PokemonManagingApp.Web.Endpoints.Owners;

public record LoginRequest(string Email, string Password);
public class LoginEndpoint(IMediator mediator, IConfiguration configuration) : EndpointBaseAsync.WithRequest<LoginRequest>.WithActionResult
{
  private readonly IMediator _mediator = mediator;
  private readonly IConfiguration _configuration = configuration;

  [HttpPost]
  [Route("/api/Owners/login")]
  [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
  [SwaggerOperation(
      Summary = "Login",
      Tags = ["Owners"]
  )]

  public override async Task<ActionResult> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
  {
    Result<OwnerDTO> result = await _mediator.Send(new LoginCommand { Email = request.Email, Password = request.Password });
    if (!result.IsSuccess) return Unauthorized();

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not found.")));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    OwnerDTO owner = result.Value;

    Claim[] claims = {
        new Claim(JwtRegisteredClaimNames.Sid, owner.Id.ToString()),
       new Claim(JwtRegisteredClaimNames.Sub, owner.UserName),
        new Claim(JwtRegisteredClaimNames.Email, owner.Email),
        new Claim(ClaimTypes.Role, owner.Role)
    };

    JwtSecurityToken Sectoken = new JwtSecurityToken(
      issuer: _configuration["Jwt:Issuer"],
      audience: _configuration["Jwt:Audience"],
      claims: claims,
      expires: DateTime.Now.AddMinutes(30),
      signingCredentials: credentials
    );

    string token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

    if (token is null) return Unauthorized();

    return Ok(new
    {
      AccessToken = token,
      ExpiresIn = 30,
    });
  }
}