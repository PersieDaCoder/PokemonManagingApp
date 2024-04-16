using PokemonManagingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.Infrastructure.Data.FakeDataGenerator;

namespace PokemonManagingApp.Infrastructure.Data;

public class ApplicationDBContext : DbContext
{
    public DbSet<Pokemon> Pokemons => Set<Pokemon>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<PokemonCategory> PokemonCategories => Set<PokemonCategory>();
    public DbSet<PokemonOwner> PokemonOwners => Set<PokemonOwner>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Gym> Gyms => Set<Gym>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=(local);Database=PokemonDatabase;User Id=sa;Password=12345;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Country[] countries = GenerateCountries.InitializeDataForCountry();
        Gym[] gyms = GenerateGyms.InitializeDataForGyms();
        Category[] categories = GenerateCategories.InitializeDataForCategories();
        Pokemon[] pokemons = GeneratePokemons.InitializeDataForPokemons();
        Owner[] owners = GenerateOwners.InitializeDataForOwners(countries,gyms);
        PokemonOwner[] pokemonOwners = GeneratePokemonOwners.InitiallizeDataForPokemonOwners(pokemons,owners);
        PokemonCategory[] pokemonCategories = GeneratePokemonCategories.InitializeDataForPokemonCategories(pokemons,categories);
        Review[] reviews = GenerateReviews.InitializeDataForReviews(pokemons,owners);
        modelBuilder.HasDefaultSchema("PokemonDB");
        modelBuilder.Entity<Country>().HasData(countries);
        modelBuilder.Entity<Gym>().HasData(gyms);
        modelBuilder.Entity<Owner>().HasData(owners);
        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity<Pokemon>().HasData(pokemons);
        modelBuilder.Entity<PokemonOwner>().HasData(pokemonOwners);
        modelBuilder.Entity<PokemonCategory>().HasData(pokemonCategories);
        modelBuilder.Entity<Review>().HasData(reviews);
    }
}