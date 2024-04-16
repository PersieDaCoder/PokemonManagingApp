using Bogus;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;
public static class GeneratePokemonCategories
{
    public static PokemonCategory[] InitializeDataForPokemonCategories(Pokemon[] pokemons, Category[] categories)
    {
        return new Faker<PokemonCategory>()
        .UseSeed(1)
        .UseDateTimeReference(new DateTime(2021, 1, 1))
        .RuleFor(pokemonCategory => pokemonCategory.Id, f => f.Random.Guid())
        .RuleFor(pokemonCategory => pokemonCategory.PokemonId, f => f.PickRandom(pokemons).Id)
        .RuleFor(pokemonCategory => pokemonCategory.CategoryId, f => f.PickRandom(categories).Id)
        .RuleFor(pokemonCategory => pokemonCategory.CreatedAt, f => f.Date.Past())
        .RuleFor(pokemonCategory => pokemonCategory.IsDeleted, f => f.Random.Bool())
        .Generate(50).ToArray();
    }
}