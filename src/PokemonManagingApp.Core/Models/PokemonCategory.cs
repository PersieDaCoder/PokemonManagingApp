using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonManagingApp.Core.Models;
public class PokemonCategory
{
    // Properties
    [ForeignKey(nameof(Pokemon))]
    public required Guid PokemonId { get; set; }
    [ForeignKey(nameof(Category))]
    public required Guid CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Status { get; set; } = true;    
    // Navigation properties
    public Pokemon? Pokemon { get; set; }
    public Category? Category { get; set; }
}