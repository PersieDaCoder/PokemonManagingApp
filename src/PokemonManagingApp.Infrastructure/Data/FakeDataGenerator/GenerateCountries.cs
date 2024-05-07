using Bogus;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;
public static class GenerateCountries
{
  public static Country[] InitializeDataForCountry()
  {
    return new Faker<Country>()
    .UseSeed(1)
    .UseDateTimeReference(new DateTime(2024, 1, 1))
    .RuleFor(country => country.Id, f => f.Random.Guid())
    .RuleFor(country => country.Name, f => f.Address.Country())
    .RuleFor(country => country.CreatedAt, f => f.Date.Past())
    .RuleFor(country => country.IsDeleted, f => f.Random.Bool())
    .RuleFor(country => country.DeletedAt, (f, u) => u.IsDeleted ? f.Date.Past() : null!)
    .Generate(50).ToArray();
  }
}
