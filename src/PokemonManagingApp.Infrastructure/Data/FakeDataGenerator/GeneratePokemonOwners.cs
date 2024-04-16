using Bogus;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;
public static class GeneratePokemonOwners
{
    public static PokemonOwner[] InitiallizeDataForPokemonOwners(Pokemon[] pokemons, Owner[] owners)
    {
        return new Faker<PokemonOwner>()
        .UseSeed(1)
        .UseDateTimeReference(new DateTime(2021, 1, 1))
        .RuleFor(pokemonOwner => pokemonOwner.Id, f => f.Random.Guid())
        .RuleFor(pokemonOwner => pokemonOwner.PokemonId, f => f.PickRandom(pokemons).Id)
        .RuleFor(pokemonOwner => pokemonOwner.OwnerId, f => f.PickRandom(owners).Id)
        .RuleFor(pokemonOwner => pokemonOwner.CreatedAt, f => f.Date.Past())
        .RuleFor(pokemonOwner => pokemonOwner.IsDeleted, f => f.Random.Bool())
        .Generate(50).ToArray();
    }
}