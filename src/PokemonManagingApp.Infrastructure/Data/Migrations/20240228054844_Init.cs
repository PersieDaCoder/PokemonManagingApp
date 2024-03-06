using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PokemonManagingApp.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "PokemonDB");

        migrationBuilder.CreateTable(
            name: "Categories",
            schema: "PokemonDB",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Countries",
            schema: "PokemonDB",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Countries", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Pokemons",
            schema: "PokemonDB",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pokemons", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Reviewers",
            schema: "PokemonDB",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reviewers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Owners",
            schema: "PokemonDB",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Gym = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Owners", x => x.Id);
                table.ForeignKey(
                    name: "FK_Owners_Countries_CountryId",
                    column: x => x.CountryId,
                    principalSchema: "PokemonDB",
                    principalTable: "Countries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PokemonCategories",
            schema: "PokemonDB",
            columns: table => new
            {
                PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PokemonCategories", x => new { x.PokemonId, x.CategoryId });
                table.ForeignKey(
                    name: "FK_PokemonCategories_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalSchema: "PokemonDB",
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PokemonCategories_Pokemons_PokemonId",
                    column: x => x.PokemonId,
                    principalSchema: "PokemonDB",
                    principalTable: "Pokemons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Reviews",
            schema: "PokemonDB",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                ReviewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reviews", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reviews_Pokemons_PokemonId",
                    column: x => x.PokemonId,
                    principalSchema: "PokemonDB",
                    principalTable: "Pokemons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Reviews_Reviewers_ReviewerId",
                    column: x => x.ReviewerId,
                    principalSchema: "PokemonDB",
                    principalTable: "Reviewers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PokemonOwners",
            schema: "PokemonDB",
            columns: table => new
            {
                PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PokemonOwners", x => new { x.PokemonId, x.OwnerId });
                table.ForeignKey(
                    name: "FK_PokemonOwners_Owners_OwnerId",
                    column: x => x.OwnerId,
                    principalSchema: "PokemonDB",
                    principalTable: "Owners",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PokemonOwners_Pokemons_PokemonId",
                    column: x => x.PokemonId,
                    principalSchema: "PokemonDB",
                    principalTable: "Pokemons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "PokemonDB",
            table: "Categories",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                  { new Guid("361a29e0-ec56-411a-8753-4521f9088da3"), "Electric" },
                  { new Guid("8c684719-e0cb-4b00-9d42-f6fe961900f8"), "Electric" },
                  { new Guid("a8ab46d3-27cd-4c68-bec2-f73471d653f8"), "Electric" }
            });

        migrationBuilder.InsertData(
            schema: "PokemonDB",
            table: "Countries",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                  { new Guid("4c29abc4-6a42-41b1-ac20-7c97f9d28868"), "Japan" },
                  { new Guid("9a372b12-9da7-43a1-a880-c7e35556b8c4"), "America" },
                  { new Guid("d181555a-73ec-4dd5-9c77-3db18671efbb"), "Korea" }
            });

        migrationBuilder.InsertData(
            schema: "PokemonDB",
            table: "Pokemons",
            columns: new[] { "Id", "BirthDate", "Name" },
            values: new object[,]
            {
                  { new Guid("099c7edc-4e2c-4e6d-bc04-141c1549399a"), new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pikachu" },
                  { new Guid("799a8b34-c056-41fe-8ac2-ef4d906ad1dd"), new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pikachu" },
                  { new Guid("c0387583-aead-4460-a86b-0bf82c2bd518"), new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pikachu" }
            });

        migrationBuilder.InsertData(
            schema: "PokemonDB",
            table: "Reviewers",
            columns: new[] { "Id", "FirstName", "LastName" },
            values: new object[,]
            {
                  { new Guid("0219db36-6f9b-42d0-b8f6-e1b05a7d8d09"), "Master", "Rayquaza" },
                  { new Guid("348597e8-7342-420f-be37-1e3154e4547a"), "Master", "Pikachu" },
                  { new Guid("de791aa3-7df5-4c18-9f6c-c6b57132c80c"), "Master", "Charmander" }
            });

        migrationBuilder.InsertData(
            schema: "PokemonDB",
            table: "Owners",
            columns: new[] { "Id", "CountryId", "Gym", "Name" },
            values: new object[,]
            {
                  { new Guid("225b113e-b7e2-4407-8e24-c995b46ac9f5"), new Guid("4c29abc4-6a42-41b1-ac20-7c97f9d28868"), "Pallet Town", "Ash Ketchup" },
                  { new Guid("4d26f80f-4732-441f-848e-801d8db25cfc"), new Guid("9a372b12-9da7-43a1-a880-c7e35556b8c4"), "Pallet Town", "Ash Ketchup" },
                  { new Guid("b690f3c2-5502-4035-967f-a808a30e4727"), new Guid("d181555a-73ec-4dd5-9c77-3db18671efbb"), "Pallet Town", "Ash Ketchup" }
            });

        migrationBuilder.InsertData(
            schema: "PokemonDB",
            table: "PokemonCategories",
            columns: new[] { "CategoryId", "PokemonId" },
            values: new object[,]
            {
                  { new Guid("a8ab46d3-27cd-4c68-bec2-f73471d653f8"), new Guid("099c7edc-4e2c-4e6d-bc04-141c1549399a") },
                  { new Guid("361a29e0-ec56-411a-8753-4521f9088da3"), new Guid("799a8b34-c056-41fe-8ac2-ef4d906ad1dd") },
                  { new Guid("8c684719-e0cb-4b00-9d42-f6fe961900f8"), new Guid("c0387583-aead-4460-a86b-0bf82c2bd518") }
            });

        migrationBuilder.InsertData(
            schema: "PokemonDB",
            table: "Reviews",
            columns: new[] { "Id", "PokemonId", "ReviewerId", "Text", "Title" },
            values: new object[,]
            {
                  { new Guid("871b10c5-f75d-4c44-ad48-a073ada97850"), new Guid("799a8b34-c056-41fe-8ac2-ef4d906ad1dd"), new Guid("de791aa3-7df5-4c18-9f6c-c6b57132c80c"), "This game is really bad !", "Bad Game" },
                  { new Guid("beb8539c-6cef-48b7-94a1-e52ab0c9968f"), new Guid("c0387583-aead-4460-a86b-0bf82c2bd518"), new Guid("348597e8-7342-420f-be37-1e3154e4547a"), "Not good at all", "Nothing is good about this game" },
                  { new Guid("f3154f60-9c51-4f21-a166-5ff27b910e70"), new Guid("099c7edc-4e2c-4e6d-bc04-141c1549399a"), new Guid("0219db36-6f9b-42d0-b8f6-e1b05a7d8d09"), "Great Game", "Big Hit" }
            });

        migrationBuilder.InsertData(
            schema: "PokemonDB",
            table: "PokemonOwners",
            columns: new[] { "OwnerId", "PokemonId" },
            values: new object[,]
            {
                  { new Guid("4d26f80f-4732-441f-848e-801d8db25cfc"), new Guid("099c7edc-4e2c-4e6d-bc04-141c1549399a") },
                  { new Guid("b690f3c2-5502-4035-967f-a808a30e4727"), new Guid("799a8b34-c056-41fe-8ac2-ef4d906ad1dd") },
                  { new Guid("225b113e-b7e2-4407-8e24-c995b46ac9f5"), new Guid("c0387583-aead-4460-a86b-0bf82c2bd518") }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Owners_CountryId",
            schema: "PokemonDB",
            table: "Owners",
            column: "CountryId");

        migrationBuilder.CreateIndex(
            name: "IX_PokemonCategories_CategoryId",
            schema: "PokemonDB",
            table: "PokemonCategories",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_PokemonOwners_OwnerId",
            schema: "PokemonDB",
            table: "PokemonOwners",
            column: "OwnerId");

        migrationBuilder.CreateIndex(
            name: "IX_Reviews_PokemonId",
            schema: "PokemonDB",
            table: "Reviews",
            column: "PokemonId");

        migrationBuilder.CreateIndex(
            name: "IX_Reviews_ReviewerId",
            schema: "PokemonDB",
            table: "Reviews",
            column: "ReviewerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PokemonCategories",
            schema: "PokemonDB");

        migrationBuilder.DropTable(
            name: "PokemonOwners",
            schema: "PokemonDB");

        migrationBuilder.DropTable(
            name: "Reviews",
            schema: "PokemonDB");

        migrationBuilder.DropTable(
            name: "Categories",
            schema: "PokemonDB");

        migrationBuilder.DropTable(
            name: "Owners",
            schema: "PokemonDB");

        migrationBuilder.DropTable(
            name: "Pokemons",
            schema: "PokemonDB");

        migrationBuilder.DropTable(
            name: "Reviewers",
            schema: "PokemonDB");

        migrationBuilder.DropTable(
            name: "Countries",
            schema: "PokemonDB");
    }
}
