using PokemonManagingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using PokemonManagingApp.UseCases.Enums;

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
    public DbSet<OwnerReview> OwnerReviews => Set<OwnerReview>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=PERSIEDAGAMER\\PERSIEPC;Database=PokemonDatabase;User Id=sa;Password=25102003;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonCategory>().HasKey(pc => new { pc.PokemonId, pc.CategoryId });
        modelBuilder.Entity<PokemonOwner>().HasKey(po => new { po.PokemonId, po.OwnerId });
        modelBuilder.Entity<OwnerReview>().HasKey(or => new { or.OwnerId, or.ReviewId });
        modelBuilder.HasDefaultSchema("PokemonDB");
        {
            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    CreatedAt = new DateTime(1996, 1, 1),
                    Email = "thinhdpham2510@gmail.com",
                    Password = "Ph@mDucThinh25102003",
                    GymId = Guid.Parse("f1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b"),
                    CountryId = Guid.Parse("4c29abc4-6a42-41b1-ac20-7c97f9d28868"),
                    UserName = "PersieDaGamer",
                    Role = (int)RoleEnum.Admin,
                }
            );
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                {
                    Id = Guid.Parse("c0387583-aead-4460-a86b-0bf82c2bd518"),
                    Name = "Pikachu",
                    BirthDate = new DateTime(1996, 1, 1),
                },
                new Pokemon
                {
                    Id = Guid.Parse("099c7edc-4e2c-4e6d-bc04-141c1549399a"),
                    Name = "Rayquaza",
                    BirthDate = new DateTime(1996, 1, 1)
                },
                new Pokemon
                {
                    Id = Guid.Parse("799a8b34-c056-41fe-8ac2-ef4d906ad1dd"),
                    Name = "Charmander",
                    BirthDate = new DateTime(1996, 1, 1)
                });

            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = Guid.Parse("4c29abc4-6a42-41b1-ac20-7c97f9d28868"),
                    Name = "Japan"
                },
                new Country
                {
                    Id = Guid.Parse("9a372b12-9da7-43a1-a880-c7e35556b8c4"),
                    Name = "America"
                },
                new Country
                {
                    Id = Guid.Parse("d181555a-73ec-4dd5-9c77-3db18671efbb"),
                    Name = "Korea"
                });

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = Guid.Parse("8c684719-e0cb-4b00-9d42-f6fe961900f8"),
                    Name = "Electric",
                },
                new Category
                {
                    Id = Guid.Parse("a8ab46d3-27cd-4c68-bec2-f73471d653f8"),
                    Name = "Dragon",
                },
                new Category
                {
                    Id = Guid.Parse("361a29e0-ec56-411a-8753-4521f9088da3"),
                    Name = "Fire",
                });
            modelBuilder.Entity<Gym>().HasData(
                new Gym
                {
                    Id = Guid.Parse("f1b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b"),
                    Name = "Pallet Town Gym"
                },
                new Gym
                {
                    Id = Guid.Parse("f2b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b"),
                    Name = "Viridian City Gym"
                },
                new Gym
                {
                    Id = Guid.Parse("f3b3b3b4-1b3b-4b3b-8b3b-1b3b3b3b3b3b"),
                    Name = "Pewter City Gym"
                });
            modelBuilder.Entity<PokemonCategory>().HasData(
                new PokemonCategory
                {
                    PokemonId = Guid.Parse("c0387583-aead-4460-a86b-0bf82c2bd518"),
                    CategoryId = Guid.Parse("8c684719-e0cb-4b00-9d42-f6fe961900f8")
                },
                new PokemonCategory
                {
                    PokemonId = Guid.Parse("099c7edc-4e2c-4e6d-bc04-141c1549399a"),
                    CategoryId = Guid.Parse("a8ab46d3-27cd-4c68-bec2-f73471d653f8")
                },
                new PokemonCategory
                {
                    PokemonId = Guid.Parse("799a8b34-c056-41fe-8ac2-ef4d906ad1dd"),
                    CategoryId = Guid.Parse("361a29e0-ec56-411a-8753-4521f9088da3")
                });
        }
    }
}