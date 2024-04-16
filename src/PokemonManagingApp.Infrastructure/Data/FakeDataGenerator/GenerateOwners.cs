namespace PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;

using Bogus;
using PokemonManagingApp.Core.Models;

public static class GenerateOwners
{
    public static Owner[] InitializeDataForOwners(Country[] countries, Gym[] gyms)
    {
        return new Faker<Owner>()
        .UseSeed(1)
        .UseDateTimeReference(new DateTime(2021, 1, 1))
        .RuleFor(owner => owner.Id, f => f.Random.Guid())
        .RuleFor(owner => owner.UserName, f => f.Person.FullName)
        .RuleFor(owner => owner.Email, f => f.Person.Email)
        .RuleFor(owner => owner.Password, f => f.Internet.Password())
        .RuleFor(owner => owner.CreatedAt, f => f.Date.Past())
        .RuleFor(owner => owner.Role, f => f.PickRandom(0,1,2))
        .RuleFor(owner => owner.IsDeleted, f => f.Random.Bool())
        .RuleFor(owner => owner.CountryId, f => f.PickRandom(countries).Id)
        .RuleFor(owner => owner.GymId, f => f.PickRandom(gyms).Id)
        .Generate(50).ToArray();
    }
}