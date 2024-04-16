using Bogus;
using PokemonManagingApp.Core.Models;

namespace PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;
public static class GenerateReviews
{
    public static Review[] InitializeDataForReviews(Pokemon[] pokemons, Owner[] owners)
    {
        return new Faker<Review>()
        .UseSeed(1)
        .UseDateTimeReference(new DateTime(2021, 1, 1))
        .RuleFor(review => review.Id, f => f.Random.Guid())
        .RuleFor(review => review.PokemonId, f => f.PickRandom(pokemons).Id)
        .RuleFor(review => review.OwnerId, f => f.PickRandom(owners).Id)
        .RuleFor(review => review.CreatedAt, f => f.Date.Past())
        .RuleFor(review => review.Title, f => f.Lorem.Sentence(10))
        .RuleFor(review => review.Text, f => f.Lorem.Sentence(100))
        .RuleFor(review => review.IsDeleted, f => f.Random.Bool())
        .RuleFor(review => review.DeletedAt, (f, u) => u.IsDeleted ? f.Date.Past() : null!)
        .Generate(50).ToArray();
    }
}