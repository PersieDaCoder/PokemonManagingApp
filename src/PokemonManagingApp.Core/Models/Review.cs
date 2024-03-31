using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonManagingApp.Core.Models;

public class Review : BaseEntity
{
  // Properties
  [MaxLength(50)]
  public required string Title { get; set; }
  [MaxLength(50)]
  public string Text { get; set; } = string.Empty;
  public required Guid PokemonId { get; set; }
  public required Guid OwnerId { get; set; }
  // Navigation properties
  [ForeignKey(nameof(PokemonId))]
  public Pokemon Pokemon { get; set; } = null!;
  [ForeignKey(nameof(OwnerId))]
  public Owner Owner { get; set; } = null!;

}