using Bogus;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;
public static class GenerateGyms
{
    private readonly static string[] _pokemonGyms = { "Pewter City Gym", "Cerulean City Gym", "Vermilion City Gym", "Celadon City Gym", "Fuchsia City Gym", "Saffron City Gym", "Cinnabar Island Gym", "Viridian City Gym" };
    public static Gym[] InitializeDataForGyms()
    {
        return new Faker<Gym>()
        .UseSeed(1)
        .UseDateTimeReference(new DateTime(2021, 1, 1))
        .RuleFor(gym => gym.Id, f => f.Random.Guid())
        .RuleFor(gym => gym.Name, f => f.PickRandom(_pokemonGyms))
        .RuleFor(gym => gym.CreatedAt, f => f.Date.Past())
        .RuleFor(gym => gym.IsDeleted, f => f.Random.Bool())
        .Generate(50).ToArray();
    }
}