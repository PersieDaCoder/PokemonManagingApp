using System.ComponentModel.DataAnnotations;

namespace PokemonManagingApp.Core.Models;

public class Pokemon : BaseEntity
{
    // Properties
    [MaxLength(50)]
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; } = DateTime.UtcNow;
    // Properties
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();

}