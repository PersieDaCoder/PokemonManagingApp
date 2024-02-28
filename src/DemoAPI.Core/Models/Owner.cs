using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI.Core.Models;

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
    
    public Country? Country { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
}