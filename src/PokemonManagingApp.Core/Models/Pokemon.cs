using System.ComponentModel.DataAnnotations;

namespace PokemonManagingApp.Core.Models;

public class Pokemon
{
    // Properties
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = true;
    // Properties
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();

}