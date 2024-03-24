using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonManagingApp.Core.Models;

public class PokemonOwner
{
    [Key]
    [ForeignKey(nameof(Pokemon))]
    public required Guid PokemonId { get; set; }
    [Key]
    [ForeignKey(nameof(Owner))]
    public required Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Status { get; set; } = true;

    public Pokemon? Pokemon { get; set; }
    public Owner? Owner { get; set; }
}