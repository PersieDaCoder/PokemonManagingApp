using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonManagingApp.Core.Models;

public class Owner
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string Gym { get; set; }
    [ForeignKey(nameof(Country))]
    public required Guid CountryId { get; set; }
    public bool Status { get; set; } = true;
    
    public Country Country { get; set; } = null!;
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
}