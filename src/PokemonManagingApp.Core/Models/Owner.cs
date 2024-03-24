using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonManagingApp.Core.Models;

public class Owner
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Password { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;
    [MaxLength(50)]
    public string Gym { get; set; } = string.Empty;
    [ForeignKey(nameof(Country))]
    public Guid CountryId { get; set; } = Guid.Parse("4C29ABC4-6A42-41B1-AC20-7C97F9D28868");
    public bool Status { get; set; } = true;

    public Country Country { get; set; } = null!;
    public ICollection<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
}