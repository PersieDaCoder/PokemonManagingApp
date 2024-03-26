using System.ComponentModel.DataAnnotations;


namespace PokemonManagingApp.Core.Models;

public class Country : BaseEntity
{
    // Properties
    [MaxLength(50)]
    public required string Name { get; set; }
    // Navigation properties
    public ICollection<Owner> Owners { get; set; } = new List<Owner>();
}