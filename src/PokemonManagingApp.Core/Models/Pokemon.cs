using System.ComponentModel.DataAnnotations;

namespace PokemonManagingApp.Core.Models;

public class Pokemon : BaseEntity
{
    // Properties
    [MaxLength(50)]
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; } = DateTime.UtcNow;
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required int Height { get; set; }
    public required int Weight { get; set; }
    // Properties
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();

}