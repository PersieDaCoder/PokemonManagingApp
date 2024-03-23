using Microsoft.AspNetCore.Identity;
using PokemonManagingApp.Core.Models;
using PokemonManagingApp.Infrastructure;
using PokemonManagingApp.Infrastructure.Data;
using PokemonManagingApp.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<Owner>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddApiEndpoints();

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new() { Title = "PokemonManagingApp.Web", Version = "v1" });
  c.EnableAnnotations();
});

// Add other dependency injection to the program.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddUseCases();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


app.MapGroup("/api/Owners").MapIdentityApi<Owner>();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();
app.Run();
