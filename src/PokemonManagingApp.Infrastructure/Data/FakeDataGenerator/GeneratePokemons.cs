using Bogus;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;
public static class GeneratePokemons
{
    private readonly static string[] _pokemonNames = { "Pikachu", "Charizard", "Bulbasaur", "Squirtle", "Jigglypuff", "Meowth", "Psyduck", "Eevee" };
    public static Pokemon[] InitializeDataForPokemons()
    {
        return new Faker<Pokemon>()
        .UseSeed(1)
        .UseDateTimeReference(new DateTime(2024, 1, 1))
        .RuleFor(pokemon => pokemon.Id, f => f.Random.Guid())
        .RuleFor(pokemon => pokemon.Name, f => f.PickRandom(_pokemonNames))
        .RuleFor(pokemon => pokemon.CreatedAt, f => f.Date.Past())
        .RuleFor(pokemon => pokemon.BirthDate, f => f.Date.Past())
        .RuleFor(pokemon => pokemon.IsDeleted, f => f.Random.Bool())
        .RuleFor(pokemon => pokemon.DeletedAt, (f, u) => u.IsDeleted ? f.Date.Past() : null!)
        .Generate(50).ToArray();
    }
}