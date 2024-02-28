using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Core.Models;

public class Review
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  [MaxLength(50)]
  public required string Title { get; set; }
  [MaxLength(50)]
  public string Text { get; set; } = string.Empty;
  [ForeignKey(nameof(Reviewer))]
  public required Guid ReviewerId { get; set; }
  [ForeignKey(nameof(Pokemon))]
  public required Guid PokemonId { get; set; }

  public Reviewer? Reviewer { get; set; }
  public Pokemon? Pokemon { get; set; }
}