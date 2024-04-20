using System.ComponentModel.DataAnnotations;

namespace PokemonManagingApp.Core.Models;

public class Category : BaseEntity
{
    // Properties
    [MaxLength(50)]
    public required string Name { get; set; }
    // Navigation properties
    public ICollection<PokemonCategory> PokemonCategories { get; set; } = [];
}