using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonManagingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PokemonManagingApp.Infrastructure.Data;

public class ApplicationDBContext : IdentityDbContext<Owner>
{
    public DbSet<Pokemon> Pokemons => Set<Pokemon>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<PokemonCategory> PokemonCategories => Set<PokemonCategory>();
    public DbSet<PokemonOwner> PokemonOwners => Set<PokemonOwner>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Reviewer> Reviewers => Set<Reviewer>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=DESKTOP-SVHFONB\\SQLEXPRESS;Database=PokemonDatabase;User Id=sa;Password=12345;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        modelBuilder.Entity<PokemonCategory>().HasKey(pc => new { pc.PokemonId, pc.CategoryId });
        modelBuilder.Entity<PokemonOwner>().HasKey(po => new { po.PokemonId, po.OwnerId });
        // modelBuilder.HasDefaultSchema("PokemonDB");

        // {
        //     modelBuilder.Entity<Pokemon>().HasData(
        //         new Pokemon
        //         {
        //             Id = Guid.Parse("c0387583-aead-4460-a86b-0bf82c2bd518"),
        //             Name = "Pikachu",
        //             BirthDate = new DateTime(1996, 1, 1)
        //         },
        //         new Pokemon
        //         {
        //             Id = Guid.Parse("099c7edc-4e2c-4e6d-bc04-141c1549399a"),
        //             Name = "Rayquaza",
        //             BirthDate = new DateTime(1996, 1, 1)
        //         },
        //         new Pokemon
        //         {
        //             Id = Guid.Parse("799a8b34-c056-41fe-8ac2-ef4d906ad1dd"),
        //             Name = "Charmander",
        //             BirthDate = new DateTime(1996, 1, 1)
        //         });

        //     modelBuilder.Entity<Country>().HasData(
        //         new Country
        //         {
        //             Id = Guid.Parse("4c29abc4-6a42-41b1-ac20-7c97f9d28868"),
        //             Name = "Japan"
        //         },
        //         new Country
        //         {
        //             Id = Guid.Parse("9a372b12-9da7-43a1-a880-c7e35556b8c4"),
        //             Name = "America"
        //         },
        //         new Country
        //         {
        //             Id = Guid.Parse("d181555a-73ec-4dd5-9c77-3db18671efbb"),
        //             Name = "Korea"
        //         });

        //     modelBuilder.Entity<Category>().HasData(
        //         new Category
        //         {
        //             Id = Guid.Parse("8c684719-e0cb-4b00-9d42-f6fe961900f8"),
        //             Name = "Electric",
        //         },
        //         new Category
        //         {
        //             Id = Guid.Parse("a8ab46d3-27cd-4c68-bec2-f73471d653f8"),
        //             Name = "Dragon",
        //         },
        //         new Category
        //         {
        //             Id = Guid.Parse("361a29e0-ec56-411a-8753-4521f9088da3"),
        //             Name = "Fire",
        //         });

        //     modelBuilder.Entity<Owner>().HasData(
        //         new Owner
        //         {
        //             Id = "225b113e-b7e2-4407-8e24-c995b46ac9f5",
        //             Name = "Ash",
        //             Gym = "Pallet Town",
        //             CountryId = Guid.Parse("4c29abc4-6a42-41b1-ac20-7c97f9d28868"),
        //         },
        //         new Owner
        //         {
        //             Id = "4d26f80f-4732-441f-848e-801d8db25cfc",
        //             Name = "Ketchup",
        //             Gym = "Beginner Town",
        //             CountryId = Guid.Parse("9a372b12-9da7-43a1-a880-c7e35556b8c4"),
        //         },
        //         new Owner
        //         {
        //             Id = "b690f3c2-5502-4035-967f-a808a30e4727",
        //             Name = "Satoshi",
        //             Gym = "Boss Town",
        //             CountryId = Guid.Parse("d181555a-73ec-4dd5-9c77-3db18671efbb"),
        //         }
        //     );

        //     modelBuilder.Entity<Reviewer>().HasData(
        //         new Reviewer
        //         {
        //             Id = Guid.Parse("348597e8-7342-420f-be37-1e3154e4547a"),
        //             FirstName = "Master",
        //             LastName = "Pikachu",
        //         },
        //         new Reviewer
        //         {
        //             Id = Guid.Parse("0219db36-6f9b-42d0-b8f6-e1b05a7d8d09"),
        //             FirstName = "Master",
        //             LastName = "Rayquaza",
        //         },
        //         new Reviewer
        //         {
        //             Id = Guid.Parse("de791aa3-7df5-4c18-9f6c-c6b57132c80c"),
        //             FirstName = "Master",
        //             LastName = "Charmander",
        //         }
        //     );

        //     modelBuilder.Entity<Review>().HasData(
        //         new Review
        //         {
        //             Id = Guid.Parse("beb8539c-6cef-48b7-94a1-e52ab0c9968f"),
        //             Title = "Nothing is good about this game",
        //             Text = "Not good at all",
        //             ReviewerId = Guid.Parse("348597e8-7342-420f-be37-1e3154e4547a"),
        //             PokemonId = Guid.Parse("c0387583-aead-4460-a86b-0bf82c2bd518"),
        //         },
        //         new Review
        //         {
        //             Id = Guid.Parse("f3154f60-9c51-4f21-a166-5ff27b910e70"),
        //             Title = "Big Hit",
        //             Text = "Great Game",
        //             ReviewerId = Guid.Parse("0219db36-6f9b-42d0-b8f6-e1b05a7d8d09"),
        //             PokemonId = Guid.Parse("099c7edc-4e2c-4e6d-bc04-141c1549399a"),
        //         },
        //         new Review
        //         {
        //             Id = Guid.Parse("871b10c5-f75d-4c44-ad48-a073ada97850"),
        //             Title = "Bad Game",
        //             Text = "This game is really bad !",
        //             ReviewerId = Guid.Parse("de791aa3-7df5-4c18-9f6c-c6b57132c80c"),
        //             PokemonId = Guid.Parse("799a8b34-c056-41fe-8ac2-ef4d906ad1dd"),
        //         }
        //     );

        //     modelBuilder.Entity<PokemonCategory>().HasData(
        //         new PokemonCategory
        //         {
        //             PokemonId = Guid.Parse("c0387583-aead-4460-a86b-0bf82c2bd518"),
        //             CategoryId = Guid.Parse("8c684719-e0cb-4b00-9d42-f6fe961900f8"),
        //         },
        //         new PokemonCategory
        //         {
        //             PokemonId = Guid.Parse("099c7edc-4e2c-4e6d-bc04-141c1549399a"),
        //             CategoryId = Guid.Parse("a8ab46d3-27cd-4c68-bec2-f73471d653f8"),
        //         },
        //         new PokemonCategory
        //         {
        //             PokemonId = Guid.Parse("799a8b34-c056-41fe-8ac2-ef4d906ad1dd"),
        //             CategoryId = Guid.Parse("361a29e0-ec56-411a-8753-4521f9088da3"),
        //         }
        //     );

        //     modelBuilder.Entity<PokemonOwner>().HasData(
        //         new PokemonOwner
        //         {
        //             PokemonId = Guid.Parse("c0387583-aead-4460-a86b-0bf82c2bd518"),
        //             OwnerId = "225b113e-b7e2-4407-8e24-c995b46ac9f5",
        //         },
        //         new PokemonOwner
        //         {
        //             PokemonId = Guid.Parse("099c7edc-4e2c-4e6d-bc04-141c1549399a"),
        //             OwnerId = "4d26f80f-4732-441f-848e-801d8db25cfc",
        //         },
        //         new PokemonOwner
        //         {
        //             PokemonId = Guid.Parse("799a8b34-c056-41fe-8ac2-ef4d906ad1dd"),
        //             OwnerId = "b690f3c2-5502-4035-967f-a808a30e4727",
        //         }
        //     );
        // }
    }
}