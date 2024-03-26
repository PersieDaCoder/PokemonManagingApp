using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonManagingApp.Core.Models;
public class PokemonCategory : BaseEntity
{
    // Properties
    public required Guid PokemonId { get; set; }
    public required Guid CategoryId { get; set; }
    // Navigation properties
    [ForeignKey(nameof(PokemonId))]
    public Pokemon Pokemon { get; set; } = null!;
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;
}
