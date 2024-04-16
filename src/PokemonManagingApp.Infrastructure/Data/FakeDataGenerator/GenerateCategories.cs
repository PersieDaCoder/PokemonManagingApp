using Bogus;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;
public static class GenerateCategories
{
    private readonly static string[] _pokemonTypes = { "Fire", "Water", "Grass", "Electric", "Psychic", "Ice", "Dragon", "Dark", "Fairy" };
    public static Category[] InitializeDataForCategories()
    {
        return new Faker<Category>()
        .UseSeed(1)
        .UseDateTimeReference(new DateTime(2021, 1, 1))
        .RuleFor(category => category.Id, f => f.Random.Guid())
        .RuleFor(category => category.Name, f => f.PickRandom(_pokemonTypes))
        .RuleFor(category => category.CreatedAt, f => f.Date.Past())
        .RuleFor(category => category.IsDeleted, f => f.Random.Bool())
        .RuleFor(category => category.DeletedAt, (f, u) => u.IsDeleted ? f.Date.Past() : null!)
        .Generate(50).ToArray();
    }
}