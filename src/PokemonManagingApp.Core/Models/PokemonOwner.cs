using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonManagingApp.Core.Models;

public class PokemonOwner : BaseEntity
{
    [ForeignKey(nameof(Pokemon))]
    public required Guid PokemonId { get; set; }
    [ForeignKey(nameof(Owner))]
    public required Guid OwnerId { get; set; }

    public Pokemon Pokemon { get; set; } = null!;
    public Owner Owner { get; set; } = null!;
}