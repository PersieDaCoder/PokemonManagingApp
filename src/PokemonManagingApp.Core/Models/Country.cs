using System.ComponentModel.DataAnnotations;


namespace PokemonManagingApp.Core.Models;

public class Country
{
    // Properties
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public required string Name { get; set; }
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    // Navigation properties
    public ICollection<Owner> Owners { get; set; } = new List<Owner>();
}