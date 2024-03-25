using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonManagingApp.Core.Models;
public class PokemonCategory
{
    // Properties
    public required Guid PokemonId { get; set; }
    public required Guid CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = true;
    // Navigation properties
    [ForeignKey(nameof(PokemonId))]
    public Pokemon Pokemon { get; set; } = null!;
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;
}
