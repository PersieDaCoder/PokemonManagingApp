using System.ComponentModel.DataAnnotations;

namespace PokemonManagingApp.Core.Models;

public class Category
{
    // Properties
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public required string Name { get; set; }
    public bool Status { get; set; } = true;
    // Navigation properties
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();
}